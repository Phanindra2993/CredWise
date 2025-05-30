import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoanService } from '../../../_services/loan.service';
import { Loan } from '../../../_models/loan.model';
import { PersonalLoanRequest, HomeLoanRequest, GoldLoanRequest } from '../../../_models/loan-application.model';
import { SuccessModalComponent } from '../../shared/components/success-modal/success-modal.component';
import { AuthService } from '../../../_services/auth.service';
import { Subject } from 'rxjs';
import { distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-apply-form',
  imports: [CommonModule, ReactiveFormsModule, FormsModule, SuccessModalComponent],
  templateUrl: './apply-form.component.html',
  styleUrl: './apply-form.component.scss'
})
export class ApplyFormComponent implements OnInit, OnDestroy {
  loanType: string = '';
  loanForm!: FormGroup;
  loanDetails: Loan | undefined;
  availableLoanTypes: { value: string; label: string }[] = [];
  submitted = false;
  fileError = '';
  isSubmitting = false;
  submissionError = '';
  showSuccessModal = false;
  genderOptions: { value: string, label: string }[] = [];
  employmentTypeOptions: { value: string, label: string }[] = [];
  isLoggedIn = false;

  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private loanService: LoanService,
    private router: Router,
    public authService: AuthService
  ) {}

  ngOnInit() {
    this.initializeForm();
    this.checkAuthentication();
    this.fetchLoanProducts();
    this.loadLoanDetails();
    this.fetchGenderOptions();
    this.fetchEmploymentTypeOptions();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  initializeForm() {
    this.loanForm = this.fb.group({
      loanType: ['', [Validators.required]],
      // Common fields
      requestedAmount: ['', [
        Validators.required,
        Validators.min(1000),
        Validators.max(5000000)
      ]],
      requestedTenure: ['', [
        Validators.required,
        Validators.min(1),
        Validators.max(60)
      ]],
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
      
      // Home Loan specific
      downPayment: ['', [
        Validators.min(0),
        Validators.max(100)
      ]],
      propertyAddress: ['', [
        Validators.maxLength(200)
      ]],
      propertyPapers: [null],
      
      // Personal Loan specific
      salarySlips: [null],
      
      // Gold Loan specific
      goldPurity: ['', [
        Validators.required,
        Validators.pattern(/^(24|22|18)K$/)
      ]],
      goldWeight: ['', [
        Validators.min(0)
      ]],
      goldValuation: [null],
      gender: ['', Validators.required],
      dob: ['', Validators.required],
      aadhaar: ['', [
        Validators.required,
        Validators.pattern(/^[0-9]{12}$/)
      ]],
      address: ['', [
        Validators.required,
        Validators.maxLength(500)
      ]],
      income: ['', [
        Validators.required,
        Validators.min(0)
      ]],
      employmentType: ['', Validators.required]
    });

    this.loanForm.get('loanType')?.valueChanges.pipe(
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe(type => {
      this.loanType = type;
      this.setValidatorsForLoanType(type);
    });
  }

  checkAuthentication() {
    this.isLoggedIn = !!this.authService.currentUserValue;
    if (this.isLoggedIn) {
      const user = this.authService.currentUserValue;
      if (user) {
        this.loanForm.patchValue({
          name: user.firstName + (user.lastName ? ' ' + user.lastName : ''),
          email: user.email,
          phone: user.phoneNumber
        });
      }
    }
  }

  fetchLoanProducts() {
    this.loanService.getLoanProducts().pipe(takeUntil(this.destroy$)).subscribe({
      next: (loans) => {
        const uniqueLoanTypes = [...new Set(loans.map(loan => loan.loanType))];
        this.availableLoanTypes = uniqueLoanTypes.map(type => ({
          value: type,
          label: type.charAt(0) + type.slice(1).toLowerCase() + ' Loan'
        }));
      },
      error: (error) => {
        console.error('Error fetching loan types:', error);
      }
    });
  }

  loadLoanDetails() {
    const loanId = Number(this.route.snapshot.paramMap.get('id'));
    if (loanId) {
      this.loanService.getLoanProductById(loanId).pipe(takeUntil(this.destroy$)).subscribe({
        next: (loan) => {
          if (loan) {
            this.loanDetails = loan;
            this.loanType = loan.loanType;
            this.loanForm.patchValue({ loanType: loan.loanType });
            this.setValidatorsForLoanType(loan.loanType);
          }
        },
        error: (error) => {
          console.error('Error fetching loan details:', error);
        }
      });
    }
  }

  fetchGenderOptions() {
    this.loanService.getGenderOptions().pipe(takeUntil(this.destroy$)).subscribe({
      next: (options) => this.genderOptions = options,
      error: () => this.genderOptions = []
    });
  }

  fetchEmploymentTypeOptions() {
    this.loanService.getEmploymentTypeOptions().pipe(takeUntil(this.destroy$)).subscribe({
      next: (options) => {
        this.employmentTypeOptions = options;
        console.log('employment type options', this.employmentTypeOptions);
      },
      error: (error) => {
        console.log('error fetching employment type options', error);
      }

    });
  }

  setValidatorsForLoanType(type: string) {
    if (this.loanType === type) return;

    const fieldsToReset = [
      'downPayment', 'propertyPapers', 'propertyAddress', 
      'salarySlips', 'goldPurity', 'goldValuation', 'goldWeight'
    ];

    fieldsToReset.forEach(field => {
      this.loanForm.get(field)?.clearValidators();
      this.loanForm.get(field)?.reset('', { emitEvent: false });
    });

    if (type === 'HOME') {
      this.loanForm.get('downPayment')?.setValidators([
        Validators.required,
        Validators.min(0),
        Validators.max(100)
      ]);
      this.loanForm.get('propertyPapers')?.setValidators([Validators.required]);
      this.loanForm.get('propertyAddress')?.setValidators([
        Validators.required,
        Validators.maxLength(200)
      ]);
      this.loanForm.get('requestedAmount')?.setValidators([
        Validators.required,
        Validators.min(100000), // Minimum amount for home loan
        Validators.max(10000000) // Maximum amount for home loan
      ]);
      this.loanForm.get('requestedTenure')?.setValidators([
        Validators.required,
        Validators.min(12), // Minimum 12 months
        Validators.max(360) // Maximum 30 years
      ]);
    } else if (type === 'PERSONAL') {
      this.loanForm.get('salarySlips')?.setValidators([Validators.required]);
      this.loanForm.get('requestedAmount')?.setValidators([
        Validators.required,
        Validators.min(10000), 
        Validators.max(500000) 
      ]);
      this.loanForm.get('requestedTenure')?.setValidators([
        Validators.required,
        Validators.min(3),
        Validators.max(60)
      ]);
    } else if (type === 'GOLD') {
      this.loanForm.get('goldPurity')?.setValidators([
        Validators.required,
        Validators.pattern(/^(24|22|18)K$/)
      ]);
      this.loanForm.get('goldWeight')?.setValidators([
        Validators.required,
        Validators.min(0.1),
        Validators.max(1000)
      ]);
      this.loanForm.get('goldValuation')?.setValidators([Validators.required]);
      this.loanForm.get('requestedAmount')?.setValidators([
        Validators.required,
        Validators.min(1000),
        Validators.max(1000000)
      ]);
      this.loanForm.get('requestedTenure')?.setValidators([
        Validators.required,
        Validators.min(3),
        Validators.max(36)
      ]);
    }

    fieldsToReset.forEach(field => {
      this.loanForm.get(field)?.updateValueAndValidity({ onlySelf: true, emitEvent: false });
    });
  }

  onFileChange(event: any, field: string) {
    const file = event.target.files[0];
    if (file) {
      if (file.size > 5 * 1024 * 1024) {
        this.fileError = 'File size should not exceed 5MB';
        this.loanForm.get(field)?.setValue(null);
        return;
      }

      const allowedTypes = ['.pdf', '.doc', '.docx', '.jpg', '.jpeg', '.png', '.gif', '.bmp', '.tiff', '.webp'];
      const fileExtension = '.' + file.name.split('.').pop()?.toLowerCase();
      if (!allowedTypes.includes(fileExtension)) {
        this.fileError = 'Only PDF and Word documents are allowed';
        this.loanForm.get(field)?.setValue(null);
        return;
      }

      this.fileError = '';
      this.loanForm.get(field)?.setValue(file);
    }
  }

  getErrorMessage(controlName: string): string {
    const control = this.loanForm.get(controlName);
    if (!control || !control.errors) return '';

    if (control.errors['required']) return 'This field is required';
    if (control.errors['email']) return 'Please enter a valid email address';
    if (control.errors['pattern']) {
      if (controlName === 'phone') return 'Please enter a valid 10-digit phone number starting with 6-9';
      if (controlName === 'name') return 'Name should only contain letters and spaces';
      if (controlName === 'email') return 'Please enter a valid email address';
      if (controlName === 'aadhaar') return 'Aadhaar must be 12 digits';
      if (controlName === 'goldPurity') return 'Gold purity must be 24K, 22K, or 18K';
    }
    if (control.errors['min']) {
      if (controlName === 'requestedAmount') {
        if (this.loanType === 'PERSONAL') return 'Minimum amount is ₹10,000';
        return 'Minimum amount is ₹1,000';
      }
      if (controlName === 'requestedTenure') {
        if (this.loanType === 'PERSONAL') return 'Minimum tenure is 3 months';
        return 'Minimum tenure is 1 month';
      }
      if (controlName === 'downPayment') return 'Down payment cannot be negative';
      if (controlName === 'goldWeight') return 'Weight cannot be negative';
      if (controlName === 'income') return 'Income must be positive';
    }
    if (control.errors['max']) {
      if (controlName === 'requestedAmount') {
        if (this.loanType === 'PERSONAL') return 'Maximum amount is ₹5,00,000';
        return 'Maximum amount is ₹1,00,00,000';
      }
      if (controlName === 'requestedTenure') {
        if (this.loanType === 'PERSONAL') return 'Maximum tenure is 60 months';
        return 'Maximum tenure is 360 months';
      }
      if (controlName === 'downPayment') return 'Down payment cannot exceed 100';
    }
    if (control.errors['maxlength']) {
      if (controlName === 'name') return 'Name cannot exceed 50 characters';
      if (controlName === 'propertyAddress') return 'Address cannot exceed 200 characters';
      if (controlName === 'address') return 'Address cannot exceed 500 characters';
    }

    return 'Invalid input';
  }

  submitForm() {
    this.submitted = true;
    if (this.loanForm.valid) {
      this.isSubmitting = true;
      this.submissionError = '';  

      if (this.loanType === 'PERSONAL') {
        this.submitPersonalLoan();
      } else if (this.loanType === 'HOME') {
        this.submitHomeLoan();
      } else if (this.loanType === 'GOLD') {
        this.submitGoldLoan();
      }
    } else {
      this.markAllAsTouched();
    }
  }

  private submitPersonalLoan() {
    const file: File = this.loanForm.value.salarySlips; 
    const user = this.authService.currentUserValue; 

    if (!user?.userId) {
      this.handleSubmissionError('User ID is missing. Please log in again.');
      return;
    }

    if (!this.loanDetails?.id) {
      this.handleSubmissionError('Loan product ID is missing.');
      return;
    }

    if (!file) {
      this.handleSubmissionError('Please upload salary slips.');
      return;
    }

    // Format date to match API expectation (YYYY-MM-DD)
    const formattedDob = this.loanForm.value.dob ? new Date(this.loanForm.value.dob).toISOString().split('T')[0] : '';

    const payload: PersonalLoanRequest = {
      userId: user.userId,
      loanProductId: this.loanDetails.id,
      requestedAmount: Number(this.loanForm.value.requestedAmount),
      requestedTenure: Number(this.loanForm.value.requestedTenure),
      gender: this.loanForm.value.gender,
      dob: formattedDob,
      aadhaar: this.loanForm.value.aadhaar,
      address: this.loanForm.value.address,
      income: Number(this.loanForm.value.income),
      employmentType: this.loanForm.value.employmentType,
      createdBy: user.email || user.firstName
    };

    console.log('Submitting personal loan application with payload:', payload);
    console.log('Auth token:', this.authService.gettoken());

    // First submit the loan application
    this.loanService.applyPersonalLoan(payload)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          console.log('Loan application response:', response);
          if (response && response.loanApplicationId) {
            // Then upload the file with the correct loan application ID
            console.log('Uploading file with loanApplicationId:', response.loanApplicationId);
            
            const formData = new FormData();
            formData.append('File', file);
            formData.append('LoanProductId', payload.loanProductId.toString());
            formData.append('LoanApplicationId', response.loanApplicationId.toString());
            formData.append('DocumentName', 'SalarySlips.pdf');

    this.loanService.uploadPersonalLoanFile(
      file,
              payload.loanProductId,
              response.loanApplicationId,
              'SalarySlips.pdf'
    ).pipe(takeUntil(this.destroy$)).subscribe({
      next: (fileResponse) => {
                console.log('File upload response:', fileResponse);
        if (fileResponse.success) { 
                  this.isSubmitting = false;
                  this.showSuccessModal = true;
                } else {
                  this.handleSubmissionError(fileResponse.message || 'File upload failed. Please try again.');
                }
              },
              error: (error) => {
                console.error('File upload error:', error);
                this.handleSubmissionError(
                  error.error?.message || 
                  error.message || 
                  'File upload failed. Please try again.'
                );
              }
            });
          } else {
            this.handleSubmissionError('Failed to create loan application. Please try again.');
          }
        },
        error: (error) => {
          console.error('Loan application error:', error);
          console.error('Error details:', {
            status: error.status,
            statusText: error.statusText,
            error: error.error,
            message: error.message
          });
          
          if (error.status === 401) {
            this.handleSubmissionError('Your session has expired. Please log in again.');
            this.authService.logout();
            this.router.navigate(['/login']);
          } else {
            this.handleSubmissionError(
              error.error?.message || 
              error.message || 
              'Failed to submit loan application. Please try again.'
            );
          }
        }
      });
  }

  private submitHomeLoan() {
    const file: File = this.loanForm.value.propertyPapers; 
    const user = this.authService.currentUserValue; 

    if (!user?.userId) {
      this.handleSubmissionError('User ID is missing. Please log in again.');
      return;
    }

    if (!this.loanDetails?.id) {
      this.handleSubmissionError('Loan product ID is missing.');
      return;
    }

    if (!file) {
      this.handleSubmissionError('Please upload property papers.');
      return;
    }

    // Format date to match API expectation (YYYY-MM-DD)
    const formattedDob = this.loanForm.value.dob ? new Date(this.loanForm.value.dob).toISOString().split('T')[0] : '';

    const payload: HomeLoanRequest = {
      userId: user.userId,
      loanProductId: this.loanDetails.id,
      requestedAmount: Number(this.loanForm.value.requestedAmount),
      requestedTenure: Number(this.loanForm.value.requestedTenure),
      gender: this.loanForm.value.gender,
      dob: formattedDob,
      aadhaar: this.loanForm.value.aadhaar,
      address: this.loanForm.value.address,
      income: Number(this.loanForm.value.income),
      employmentType: this.loanForm.value.employmentType,
      createdBy: user.email || user.firstName,
      propertyAddress: this.loanForm.value.propertyAddress,
      downPaymentPercentage: Number(this.loanForm.value.downPayment)
    };

    console.log('Submitting home loan application with payload:', payload);
    console.log('Auth token:', this.authService.gettoken());

    // First submit the loan application
    this.loanService.applyHomeLoan(payload)
            .pipe(takeUntil(this.destroy$))
            .subscribe({
              next: (response) => {
          console.log('Home loan application response:', response);
          if (response && response.loanApplicationId) {
            // Then upload the file with the correct loan application ID
            console.log('Uploading file with loanApplicationId:', response.loanApplicationId);
            
            const formData = new FormData();
            formData.append('File', file);
            formData.append('LoanProductId', payload.loanProductId.toString());
            formData.append('LoanApplicationId', response.loanApplicationId.toString());
            formData.append('DocumentName', 'PropertyPapers.pdf');

            this.loanService.uploadPersonalLoanFile(
              file,
              payload.loanProductId,
              response.loanApplicationId,
              'PropertyPapers.pdf'
            ).pipe(takeUntil(this.destroy$)).subscribe({
              next: (fileResponse) => {
                console.log('File upload response:', fileResponse);
                if (fileResponse.success) {
                  this.isSubmitting = false;
                  this.showSuccessModal = true;
                } else {
                  this.handleSubmissionError(fileResponse.message || 'File upload failed. Please try again.');
                }
              },
              error: (error) => {
                console.error('File upload error:', error);
                this.handleSubmissionError(
                  error.error?.message || 
                  error.message || 
                  'File upload failed. Please try again.'
                );
              }
            });
        } else {
            this.handleSubmissionError('Failed to create home loan application. Please try again.');
          }
        },
        error: (error) => {
          console.error('Home loan application error:', error);
          console.error('Error details:', {
            status: error.status,
            statusText: error.statusText,
            error: error.error,
            message: error.message
          });
          
          if (error.status === 401) {
            this.handleSubmissionError('Your session has expired. Please log in again.');
            this.authService.logout();
            this.router.navigate(['/login']);
          } else {
            this.handleSubmissionError(
              error.error?.message || 
              error.message || 
              'Failed to submit home loan application. Please try again.'
            );
          }
        }
      });
  }

  private submitGoldLoan() {
    const file: File = this.loanForm.value.goldValuation; 
    const user = this.authService.currentUserValue; 

    if (!user?.userId) {
      this.handleSubmissionError('User ID is missing. Please log in again.');
      return;
    }

    if (!this.loanDetails?.id) {
      this.handleSubmissionError('Loan product ID is missing.');
      return;
    }

    if (!file) {
      this.handleSubmissionError('Please upload gold valuation report.');
      return;
    }

    // Format date to match API expectation (YYYY-MM-DD)
    const formattedDob = this.loanForm.value.dob ? new Date(this.loanForm.value.dob).toISOString().split('T')[0] : '';

    // Format gender to uppercase
    const formattedGender = this.loanForm.value.gender?.toUpperCase();

    // Validate gold purity format
    const goldPurity = this.loanForm.value.goldPurity;
    if (!/^(24|22|18)K$/.test(goldPurity)) {
      this.handleSubmissionError('Gold purity must be 24K, 22K, or 18K');
      return;
    }

    // Keep employment type in original format (Self-Employed)
    const employmentType = this.loanForm.value.employmentType;

    const payload: GoldLoanRequest = {
      userId: user.userId,
      loanProductId: this.loanDetails.id,
      requestedAmount: Number(this.loanForm.value.requestedAmount),
      requestedTenure: Number(this.loanForm.value.requestedTenure),
      gender: formattedGender,
      dob: formattedDob,
      aadhaar: this.loanForm.value.aadhaar,
      address: this.loanForm.value.address,
      income: Number(this.loanForm.value.income),
      employmentType: employmentType,
      createdBy: user.email || user.firstName,
      goldWeight: Number(this.loanForm.value.goldWeight),
      goldPurity: goldPurity
    };

    console.log('Submitting gold loan application with payload:', payload);
    console.log('Auth token:', this.authService.gettoken());

    // First submit the loan application
    this.loanService.applyGoldLoan(payload)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          console.log('Gold loan application response:', response);
          if (response && response.loanApplicationId) {
            // Then upload the file with the correct loan application ID
            console.log('Uploading file with loanApplicationId:', response.loanApplicationId);
            
            const formData = new FormData();
            formData.append('File', file);
            formData.append('LoanProductId', payload.loanProductId.toString());
            formData.append('LoanApplicationId', response.loanApplicationId.toString());
            formData.append('DocumentName', 'GoldValuation.pdf');

            this.loanService.uploadPersonalLoanFile(
              file,
              payload.loanProductId,
              response.loanApplicationId,
              'GoldValuation.pdf'
            ).pipe(takeUntil(this.destroy$)).subscribe({
              next: (fileResponse) => {
                console.log('File upload response:', fileResponse);
                if (fileResponse.success) {
                  this.isSubmitting = false;
                  this.showSuccessModal = true;
        } else {
          this.handleSubmissionError(fileResponse.message || 'File upload failed. Please try again.');
                }
              },
              error: (error) => {
                console.error('File upload error:', error);
                this.handleSubmissionError(
                  error.error?.message || 
                  error.message || 
                  'File upload failed. Please try again.'
                );
              }
            });
          } else {
            this.handleSubmissionError('Failed to create gold loan application. Please try again.');
        }
      },
      error: (error) => {
          console.error('Gold loan application error:', error);
          console.error('Error details:', {
            status: error.status,
            statusText: error.statusText,
            error: error.error,
            message: error.message
          });
          
          if (error.status === 401) {
            this.handleSubmissionError('Your session has expired. Please log in again.');
            this.authService.logout();
            this.router.navigate(['/login']);
          } else if (error.status === 400) {
            // Log the detailed error message from the API
            console.error('API Error Response:', error.error);
            if (error.error?.errors) {
              const errorMessages = Object.entries(error.error.errors)
                .map(([field, messages]) => `${field}: ${(messages as string[]).join(', ')}`)
                .join('\n');
              this.handleSubmissionError(errorMessages);
            } else {
              this.handleSubmissionError(
                error.error?.message || 
                'Invalid data provided. Please check all fields and try again.'
              );
            }
          } else {
            this.handleSubmissionError(
              error.error?.message || 
              error.message || 
              'Failed to submit gold loan application. Please try again.'
            );
          }
      }
    });
  }

  private handleSubmissionError(error: any) {
    this.isSubmitting = false;
    console.error('Submission error details:', error);
    console.error('Error type:', typeof error);
    console.error('Error object:', error);
    
    if (typeof error === 'string') {
      this.submissionError = error;
    } else if (error.error?.message) {
      this.submissionError = error.error.message;
    } else if (error.message) {
      this.submissionError = error.message;
    } else {
      this.submissionError = 'An unexpected error occurred. Please try again.';
    }
  }

  private markAllAsTouched() {
    Object.keys(this.loanForm.controls).forEach(key => {
      const control = this.loanForm.get(key);
      control?.markAsTouched();
    });
  }

  onModalClose() {
    this.showSuccessModal = false;
  }

  onModalOk() {
    this.showSuccessModal = false;
    this.router.navigate(['/']);
  }

  isFormValid(): boolean {
    if (!this.loanForm.valid) return false;
    
    if (this.loanType === 'HOME') {
      return !!(this.loanForm.get('downPayment')?.valid && 
                this.loanForm.get('propertyAddress')?.valid && 
                this.loanForm.get('propertyPapers')?.value);
    } else if (this.loanType === 'PERSONAL') {
      return !!(this.loanForm.get('salarySlips')?.value);
    } else if (this.loanType === 'GOLD') {
      return !!(this.loanForm.get('goldPurity')?.valid && 
                this.loanForm.get('goldWeight')?.valid && 
                this.loanForm.get('goldValuation')?.value);
    }
    
    return false;
  }
}