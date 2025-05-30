import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { LoanService } from '../../../_services/loan.service';
import { AuthService } from '../../../_services/auth.service';
import { log } from 'ng-zorro-antd/core/logger';
@Component({
  selector: 'app-loan-status',
  imports:[CommonModule],
  templateUrl: './loan-status.component.html',
  styleUrls: ['./loan-status.component.scss']
})
export class LoanStatusComponent implements OnInit {
  loanApplications: any[] = [];
  userId: number = 0;


  constructor(private loanService: LoanService, private authService: AuthService) {}

 


  ngOnInit() {
    // Dummy data for UI design
    // this.loanApplications = [
    //   {
    //     LoanApplicationId: 101,
    //     LoanProductTitle: 'Home Loan',
    //     RequestedAmount: 2500000,
    //     RequestedTenure: 120,
    //     InterestRate: 8.5,
    //     Status: 'Approved',
    //     DecisionDate: new Date('2024-06-01'),
    //     DecisionReason: null,
    //     CreatedAt: new Date('2024-05-10')
    //   },
    //   {
    //     LoanApplicationId: 102,
    //     LoanProductTitle: 'Personal Loan',
    //     RequestedAmount: 200000,
    //     RequestedTenure: 24,
    //     InterestRate: 12.0,
    //     Status: 'Rejected',
    //     DecisionDate: new Date('2024-06-05'),
    //     DecisionReason: 'Low credit score',
    //     CreatedAt: new Date('2024-05-15')
    //   },
    //   {
    //     LoanApplicationId: 103,
    //     LoanProductTitle: 'Gold Loan',
    //     RequestedAmount: 50000,
    //     RequestedTenure: 12,
    //     InterestRate: 10.5,
    //     Status: 'Processing',
    //     DecisionDate: null,
    //     DecisionReason: null,
    //     CreatedAt: new Date('2024-06-10')
    //   }
    // ]; 
    const userId = this.authService.getUserId();

    this.loanService.getUserLoanApplicationsByUserId(userId.toString()).subscribe({
      next: (data) => {
        this.loanApplications = data;
        console.log(this.loanApplications);
      },
      error: (error) => {
        console.log(error);
      }

    });
  }

  statusClass(status: string): string {
    switch (status) {
      case 'Approved': return 'approved';
      case 'Rejected': return 'rejected';
      case 'Processing': return 'processing';
      case 'Initial Review': return 'review';
      case 'Documents Collected': return 'collected';
      case 'Decision Pending': return 'pending';
      default: return '';
    }
  }
}