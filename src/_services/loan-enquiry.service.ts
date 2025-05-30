import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EligibilityEnquiry } from '../_models/eligibilty-enquiry.model';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class LoanEnquiryService {

  constructor(private http: HttpClient) { }
  private baseUrl = 'https://localhost:7037/api/LoanEnquiry';
  

  loanEligibilityEnquiry(enquiry: EligibilityEnquiry): Observable<EligibilityEnquiry  > {
    return this.http.post<EligibilityEnquiry>(`${this.baseUrl}`, enquiry);
  }
}
