import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PaymentService } from '../../../_services/payment.service';
import { CommonModule } from '@angular/common';
import { log } from 'ng-zorro-antd/core/logger';

@Component({
  selector: 'app-repayment',
  templateUrl: './repayment.component.html',
  styleUrl: './repayment.component.scss',
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule ,CommonModule  ]
})
export class RepaymentComponent {
  repaymentForm: FormGroup;
  submitted = false;
  isSubmitting = false;
  errorMsg = '';
  successMsg = '';
  repaymentId: number | null = null;
  paymentMethods: any[] = [];

  constructor(private fb: FormBuilder, private paymentService: PaymentService) {
    this.repaymentForm = this.fb.group({
       loanApplicationId: ['', Validators.required], // Uncomment if needed
      dueDate: ['', Validators.required],
      totalAmount: ['', [Validators.required, Validators.min(1)]],
      amount: ['', [Validators.required, Validators.min(1)]],
      paymentMethod: ['', Validators.required],
    });
  }
  ngOnInit(): void {

    //get employment types
    this.repaymentForm.get('loanApplicationId')?.valueChanges.subscribe((loanApplicationId) => {
      if (loanApplicationId) {
        this.paymentService.getDueInfo(loanApplicationId).subscribe({
          next: (res) => {
            if (Array.isArray(res) && res.length > 0) {
              const due = res[0];
              this.repaymentId = due.repaymentId;
              this.repaymentForm.patchValue({
                dueDate: due.dueDate,
                totalAmount: due.totalAmount,
                amount: due.totalAmount // default to full due
              });
            }
          },
          error: (err) => {
            this.errorMsg = 'Could not fetch due info.';
          }
        });
      }
    });

    //get payment methods
    this.paymentService.getPaymentMethods().subscribe((res) => {
        this.paymentMethods = res;
        // console.log('res', res);
        
        // console.log('payment methods', this.paymentMethods);
        
    });
  }

  getErrorMessage(controlName: string): string {
    const control = this.repaymentForm.get(controlName);
    if (!control || !control.errors) return '';
    if (control.errors['required']) return 'This field is required';
    if (control.errors['min']) return 'Amount must be greater than 0';
    return 'Invalid input';
  }

  submitForm() {
    this.submitted = true;
    this.errorMsg = '';
    this.successMsg = '';
    if (this.repaymentForm.valid && this.repaymentId !== null) {
      this.isSubmitting = true;
      const payload = {
        repaymentId: this.repaymentId,
        amount: this.repaymentForm.value.amount,
        paymentMethod: this.repaymentForm.value.paymentMethod
      };
      this.paymentService.postRepayment(payload).subscribe({
        next: () => {
          this.successMsg = 'Repayment submitted successfully!';
          this.repaymentForm.reset();
          this.submitted = false;
          this.isSubmitting = false;
          this.repaymentId = null;
        },
        error: (err) => {
          this.errorMsg = 'Failed to submit repayment. Please try again.';
          this.isSubmitting = false;
        }
      });
    } else {
      Object.values(this.repaymentForm.controls).forEach(control => control.markAsTouched());
    }
  }
}
