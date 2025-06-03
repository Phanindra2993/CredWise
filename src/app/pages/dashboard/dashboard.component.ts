import { Component, OnInit } from '@angular/core';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoanService } from '../../../_services/loan.service';
import { CommonModule } from '@angular/common';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { HowItWorksStep } from '../../../_models/howItWorks.model';
import { HowItWorksService } from '../../../_services/how-it-works.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { Loan, LoanResponse } from '../../../_models/loan.model';
import { LoanEnquiryService } from '../../../_services/loan-enquiry.service';
import { EligibilityEnquiry } from '../../../_models/eligibilty-enquiry.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    NzCardModule,
    CommonModule,
    NzIconModule,
    NzButtonModule,
    NzInputModule,
    NzFormModule,
    NzSelectModule,
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  loanProducts: any[] = [];
  howItWorksSteps: HowItWorksStep[] = [];
  eligibilityForm: FormGroup;
  submitted = false;
  loanPurposes: string[] = [];
  
  


  constructor(
    private loanService: LoanService,
    private fb: FormBuilder,
    private howItWorksService: HowItWorksService,
    private message: NzMessageService,
    private router: Router,
    private loanEnquiryService: LoanEnquiryService

  ) {
    this.eligibilityForm = this.fb.group({
      name: ['', [
        Validators.required,
        Validators.maxLength(50),
        Validators.pattern(/^(?=.*[a-zA-Z])[a-zA-Z\s]*$/)
      ]],
      phone: ['', [
        Validators.required,
        Validators.pattern(/^[6-9][0-9]{9}$/)
      ]],
      amount: ['', [Validators.required, Validators.min(1000)]],
      purpose: [null, [Validators.required]]
    });
  }


  ngOnInit(): void {
    //getting loan products
    this.loanService.getLoanProducts().subscribe({
      next: (response: Loan[]) => {
        this.loanProducts = response;
        this.loanPurposes = [...new Set(response.map(loan => loan.loanType))];
        // console.log(this.loanPurposes);
      },
      error: (error) => {
        console.error('Dashboard: Error loading products:', error);
      }
    });
    //getting how it works steps
    this.howItWorksService.getSteps().subscribe({
      next: (steps) => {
        console.log('steps',steps);
        this.howItWorksSteps = steps;
        // console.log('How It Works Steps loaded:', steps);
      },
      error: (error) => {
        console.error('Error loading steps:', error);
      }
    });
    //getting loan eligibility enquiry
    
   
  }

  getErrorMessage(controlName: string): string {
    const control = this.eligibilityForm.get(controlName);
    if (!control || !control.errors) return '';

    if (control.errors['required']) return 'This field is required';
    if (control.errors['maxlength']) return 'Maximum 50 characters allowed';
    if (control.errors['pattern']) {
      if (controlName === 'phone') return 'Please enter a valid 10-digit phone number starting with 6-9';
      if (controlName === 'name') return 'Name must contain only letters and spaces';
    }
    if (control.errors['min']) {
      if (controlName === 'amount') return 'Minimum amount is ₹1,000';
    }
    if (control.errors['purpose']) return 'Please select a valid purpose';
    if (control.errors['amount']) return 'Please enter a valid amount';
    
    

      
    return 'Invalid input';
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.eligibilityForm.valid) {
      const formData: EligibilityEnquiry = {
        name: this.eligibilityForm.get('name')?.value,
        phoneNumber: this.eligibilityForm.get('phone')?.value,
        loanAmount: this.eligibilityForm.get('amount')?.value,
        loanPurpose: this.eligibilityForm.get('purpose')?.value
      };

      this.loanEnquiryService.loanEligibilityEnquiry(formData).subscribe({
        next: (enquiry) => {
          console.log('Enquiry submitted successfully:', enquiry);
          this.message.success('Thank you for your interest in our loan products. We will get back to you soon.', {nzDuration: 5000});
          this.eligibilityForm.reset();
          this.submitted = false;
        },
        error: (error) => {
          console.error('Error submitting enquiry:', error);
          this.message.error('Failed to submit enquiry. Please try again.', {nzDuration: 5000});
        }
      }); 
    } else {
      Object.values(this.eligibilityForm.controls).forEach(control => control.markAsTouched());
    }
  }

  
}
