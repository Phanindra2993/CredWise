import { Component } from '@angular/core';
import { Loan } from '../../../_models/loan.model';
import { LoanService } from '../../../_services/loan.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-loan-details',
  imports: [CommonModule, FormsModule,RouterLink],
  templateUrl: './loan-details.component.html',
  styleUrls: ['./loan-details.component.scss'],
  standalone: true
})
export class LoanDetailsComponent {
  loanProductDetails: Loan | undefined;
  id: number;
  principal: number | null = null;
  annualInterestRate: number | null = null;
  loanTermMonths: number | null = null;
  emiResult: number | null = null;
  emiResultString: string = '';
  emiFormSubmitted = false;

  constructor(private loanService: LoanService, private route: ActivatedRoute) {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    console.log('Loan Details: Fetching loan with ID:', this.id, 'Type:', typeof this.id);
    
    this.loanService.getLoanProductById(this.id).subscribe({
      next: (data) => {
        this.loanProductDetails = data;
      // set annualInterestRate or loanTermMonths here; keep all fields empty initially
        if (data) {
   //       console.log('Loan Details: Setting EMI fields');
   console.log('Loan Details: Setting EMI fields',data);
          this.annualInterestRate = data.loanDetail?.interestRate || 0;
          this.loanTermMonths = data.loanDetail?.tenureMonths || 0;
        } else {
          console.error('Loan Details: No data found for ID:', this.id);
        }
      },
      error: (error) => {
        console.error('Loan Details: Error fetching loan:', error);
      }
    });
  }


  //calculate EMI 
  calculateEMI(){
    this.emiFormSubmitted = true;
    if (!this.isEmiFormValid()) return;
    this.loanService.calculateEMI(this.principal!,this.annualInterestRate!,this.loanTermMonths!).subscribe({
      next:(emiResult: {emi: number} | number)=>{
        console.log('EMI Result:',emiResult);
        this.emiResult = (emiResult && typeof emiResult === 'object' && 'emi' in emiResult) ? emiResult.emi : (typeof emiResult === 'number' ? emiResult : null);
      },
      error:(error)=>{
        console.error('Error calculating EMI:',error);
        this.emiResult = null;
      }
    })
  }

  isEmiFormValid(): boolean {
    return (
      this.principal !== null && this.principal >= 1 && this.principal <= 10000000 &&
      this.annualInterestRate !== null && this.annualInterestRate >= 1 && this.annualInterestRate <= 20 &&
      this.loanTermMonths !== null && this.loanTermMonths >= 1 && this.loanTermMonths <= 360
    );
  }

  getEmiError(field: string): string {
    if (!this.emiFormSubmitted) {
      console.log('Not submitted yet, no error for', field);
      return '';
    }
    const value = this[field as keyof this] as number | null;
    console.log('Checking error for', field, 'value:', value, 'type:', typeof value);
    if (value === null || value === undefined || value === 0) return 'This field is required';
    if (field === 'principal') {
      if (value < 1) {
        console.log('Principal too low:', value);
        return 'Principal too low:';
      }
      if (value > 10000000) {
        console.log('Principal too high:', value);
        return 'Maximum amount is ₹1,00,00,000';
      }
    }
    if (field === 'annualInterestRate') {
      if (value < 1) return 'Minimum rate is 1%';
      if (value > 20) return 'Maximum rate is 20%';
    }
    if (field === 'loanTermMonths') {
      if (value < 1) return 'Minimum tenure is 1 month';
      if (value > 360) return 'Maximum tenure is 360 months';
    }
    return '';
  }

  onPrincipalChange(value: any) {
    this.principal = value ? +value : null;
    this.emiFormSubmitted = false;
  }

  // calculateEMI() {
  //   if (this.emiAmount > 0 && this.emiInterest > 0 && this.emiTenure > 0) {
  //     const r = this.emiInterest / 12 / 100;
  //     const n = this.emiTenure;
  //     const emi = (this.emiAmount * r * Math.pow(1 + r, n)) / (Math.pow(1 + r, n) - 1);
  //     this.emiResult = emi;
  //   } else {
  //     this.emiResult = null;
  //   }
  // }
}
