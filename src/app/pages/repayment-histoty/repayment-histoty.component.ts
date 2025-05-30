import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../../_services/payment.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-repayment-histoty',
  imports: [CommonModule],
  templateUrl: './repayment-histoty.component.html',
  styleUrl: './repayment-histoty.component.scss'
})
export class RepaymentHistotyComponent implements OnInit {
  repaymentHistory: any[] = [];
  error: string = '';

  constructor(private paymentService: PaymentService) {}

  ngOnInit() {
    this.paymentService.getRepaymentHistory(0).subscribe({
      next: (data) => {
        console.log(data);
        this.repaymentHistory = data;
      },
      error: (err) => {
        this.error = err.message;
        console.log(err);
      }
    });
  }
}
