import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RepaymentHistory } from '../_models/repayment-history.model';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private postRepaymentUrl = 'https://localhost:7037/api/Repayment/pay';
  private historyUrl = 'https://localhost:7037/api/Repayment/user';
  private dueInfoUrl = 'https://localhost:7037/api/Repayment/schedule';
  private paymentMethodsUrl = 'https://localhost:7037/payment-types';
  private interestRateUrl = 'https://localhost:7001/api/LoanProduct';

  constructor(private http: HttpClient) { }

  getDueInfo(loanApplicationId: number): Observable<any> {
    return this.http.get(`${this.dueInfoUrl}/${loanApplicationId}`);
  }
  //repayment page
  postRepayment(data: any): Observable<any> {
    return this.http.post(`${this.postRepaymentUrl}`, data);
  }
  //repayment history page
  getRepaymentHistory(userId: number): Observable<RepaymentHistory[]> {
    return this.http.get<RepaymentHistory[]>(`${this.historyUrl}/${userId}/payment-history`);
  }
  getPaymentMethods(): Observable<any> {
    return this.http.get(`${this.paymentMethodsUrl}`);
  }

  getInterestRate(): Observable<any> {
    return this.http.get(`${this.interestRateUrl}`);
  }
}
