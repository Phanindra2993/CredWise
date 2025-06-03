import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SuccessModalComponent } from '../../shared/components/success-modal/success-modal.component';
import { FdService  } from '../../../_services/fd.service';
import { FDType } from '../../../_models/fd.model';
import { AuthService } from '../../../_services/auth.service';
import { Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-apply-fd',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, SuccessModalComponent],
  templateUrl: './apply-fd.component.html',
  styleUrls: ['./apply-fd.component.scss']
})
export class ApplyFdComponent implements OnInit {
  fdForm!: FormGroup;
  fdTypes: FDType[] = [];
  submitted = false;
  fileError = '';
  isSubmitting = false;
  submissionError = '';
  showSuccessModal = false;
  isLoggedIn = false;
  private calculationSubject = new Subject<any>();

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private fdService: FdService,
    public authService: AuthService
  ) {
    this.initializeForm();
    this.setupDebounceCalculation();
  }

  private setupDebounceCalculation() {
    this.calculationSubject.pipe(
      debounceTime(1000), // Wait for 1 second of inactivity
      distinctUntilChanged() // Only emit if the value has changed
    ).subscribe(({ email, fdTypeId, amount }) => {
      this.calculateFDDetails(email, fdTypeId, amount);
    });
  }

  private calculateFDDetails(email: string, fdTypeId: number, amount: number) {
    this.fdService.submitFDApplication({ email, fdTypeId, amount }).subscribe({
      next: (response: any) => {
        const formattedDate = response.maturityDate ? new Date(response.maturityDate).toLocaleDateString() : '';
        this.fdForm.patchValue({
          duration: response.duration,
          interestRate: response.interestRate,
          maturityAmount: response.maturityAmount,
          maturityDate: formattedDate
        }, { emitEvent: false });
      },
      error: () => {
        this.fdForm.patchValue({
          duration: '',
          interestRate: '',
          maturityAmount: '',
          maturityDate: ''
        }, { emitEvent: false });
      }
    });
  }

  initializeForm() {
    this.fdForm = this.fb.group({
      fdTypeId: ['', [Validators.required]],
      name: ['', [
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z\s]*$/)
      ]],
      email: ['', [
        Validators.required,
        Validators.email,
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/)
      ]],
      phone: ['', [
        Validators.required,
        Validators.pattern(/^[6-9][0-9]{9}$/)
      ]],
      amount: ['', [
        Validators.required,
        Validators.min(5000),
        Validators.max(500000)
      ]],
      duration: [{value: '', disabled: true}],
      interestRate: [{value: '', disabled: true}],
      maturityAmount: [{value: '', disabled: true}],
      maturityDate: [{value: '', disabled: true}],
      panCard: [null],
      addressProof: [null]
    });

    // Add valueChanges subscription for auto-calculation
    this.fdForm.valueChanges.subscribe(() => {
      const fdTypeId = this.fdForm.get('fdTypeId')?.value;
      const amount = this.fdForm.get('amount')?.value;
      const email = this.fdForm.get('email')?.value;

      if (
        fdTypeId &&
        amount &&
        !isNaN(amount) &&
        amount >= 5000 &&
        amount <= 500000 &&
        email
      ) {
        // Emit the values to the debounced subject
        this.calculationSubject.next({ email, fdTypeId, amount });
      } else {
        this.fdForm.patchValue({
          duration: '',
          interestRate: '',
          maturityAmount: '',
          maturityDate: ''
        }, { emitEvent: false });
      }
    });
  }

  ngOnInit() {
    // console.log(this.fdTypes)
    this.isLoggedIn = !!this.authService.currentUserValue;
    const user = this.authService.currentUserValue;
    if (user) {
      this.fdForm.patchValue({
        name: user.firstName + (user.lastName ? ' ' + user.lastName : ''),
        email: user.email,
        phone: user.phoneNumber
      });
    }
    //getting fd types
    this.fdService.getFdTypes().subscribe({
      next: (types: FDType[]) => {
        console.log('fd types',types);
        console.log("ID",types[0].fdTypeId)
        this.fdTypes = types;
      },
      error: (error: Error) => {
        console.error('Error fetching FD types:', error);
        this.submissionError = 'Error loading FD types. Please try again later.';
      }
    });
  }

  onFileChange(event: any, field: string) {
    const file = event.target.files[0];
    if (file) {
      if (file.size > 5 * 1024 * 1024) {
        this.fileError = 'File size should not exceed 5MB';
        this.fdForm.get(field)?.setValue(null);
        return;
      }

      const allowedTypes = ['.pdf', '.jpg', '.jpeg', '.png'];
      const fileExtension = '.' + file.name.split('.').pop()?.toLowerCase();
      if (!allowedTypes.includes(fileExtension)) {
        this.fileError = 'Only PDF and image files are allowed';
        this.fdForm.get(field)?.setValue(null);
        return;
      }

      this.fileError = '';
      this.fdForm.get(field)?.setValue(file);
    }
  }

  getErrorMessage(controlName: string): string {
    const control = this.fdForm.get(controlName);
    if (!control || !control.errors) return '';

    if (control.errors['required']) return 'This field is required';
    if (control.errors['email']) return 'Please enter a valid email address';
    if (control.errors['pattern']) {
      if (controlName === 'phone') return 'Please enter a valid 10-digit phone number starting with 6-9';
      if (controlName === 'name') return 'Name should only contain letters and spaces';
      if (controlName === 'email') return 'Please enter a valid email address';
    }
    if (control.errors['min']) {
      if (controlName === 'amount') return 'Minimum FD amount is ₹10,000';
    }
    if (control.errors['max']) {
      if (controlName === 'amount') return 'Maximum FD amount is ₹5,00,000';
    }
    if (control.errors['maxlength']) {
      if (controlName === 'name') return 'Name cannot exceed 50 characters';
    }

    return 'Invalid input';
  }

  submitFDApplication() {
    this.submitted = true;
    if (this.fdForm.valid) {
      const amount = this.fdForm.get('amount')?.value;
      if (amount < 10000 || amount > 500000) {
        this.submissionError = 'FD amount should be between ₹10,000 and ₹5,00,000';
        return;
      }

    //  console.log('Form submitted:', this.fdForm.value);
      this.isSubmitting = true;

      const email = this.fdForm.get('email')?.value;
      const fdTypeId = this.fdForm.get('fdTypeId')?.value;
      this.fdService.submitFDApplication({ email, fdTypeId, amount }).subscribe({
        next: (response: any) => {
     //     console.log(response);
          this.showSuccessModal = true;
          this.isSubmitting = false;

          const formattedDate = response.maturityDate ? new Date(response.maturityDate).toLocaleDateString() : '';
          // Patch the returned details into the form
          this.fdForm.patchValue({
            duration: response.duration,
            interestRate: response.interestRate,
            maturityAmount: response.maturityAmount,
            maturityDate: formattedDate
          });

          const panCardFile = this.fdForm.get('panCard')?.value;
          const addressProofFile = this.fdForm.get('addressProof')?.value;
          //here i need to upload the files to the server
          if (panCardFile) {
            this.fdService.uploadFdDocument(panCardFile, fdTypeId, response.fdApplicationId, 'PANCard').subscribe();
          }
          if (addressProofFile) {
            this.fdService.uploadFdDocument(addressProofFile, fdTypeId, response.fdApplicationId, 'AddressProof').subscribe();
          }
        },
        error: (error: any) => {
          console.error('Error submitting application:', error);
          this.submissionError = 'An error occurred while submitting the application. Please try again later.';
          this.isSubmitting = false;
        }
      });
    } else {
      Object.keys(this.fdForm.controls).forEach(key => {
        const control = this.fdForm.get(key);
        control?.markAsTouched();
      });
    }
  }

  onModalClose() {
    this.showSuccessModal = false;
  }

  onModalOk() {
    this.showSuccessModal = false;
    this.router.navigate(['/']);
  }

  isFormValid(): boolean {
    if (!this.fdForm.valid) return false;
    
    const selectedType = this.fdTypes.find(type => type.fdTypeId === this.fdForm.get('fdTypeId')?.value);
    if (!selectedType) return false;

    const amount = this.fdForm.get('amount')?.value;
    if (amount < selectedType.minAmount || amount > selectedType.maxAmount) return false;

    return true;
  }
}
