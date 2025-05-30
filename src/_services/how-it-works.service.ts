import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HowItWorksStep } from '../_models/howItWorks.model';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HowItWorksService { 

  // howItWorks=[
  //   {
  //     "id": 1,
  //     "title": "Apply for the Loan",
  //     "description": "Go to the Apply Now page enter your details for the loan application and apply for the loan."
  //   },
  //   {
  //     "id": 2,
  //     "title": "Submit Your Documents",
  //     "description": "As per the requirements of documents, submit your documents and get the response shortly."
  //   },
  //   {
  //     "id": 3,
  //     "title": "Wait for the Approval",
  //     "description": "Once the documents are submitted, your loan approval takes a few hours only."
  //   },
  //   {
  //     "id": 4,
  //     "title": "Get Disbursal",
  //     "description": "Get the disbursal directly in your linked account and use the fund as per the requirements."
  //   }
  // ]


  private apiUrl = 'https://localhost:7037/api/HowItWorks';

  constructor(private http: HttpClient) { } 

  getSteps(): Observable<HowItWorksStep[]> {
    return this.http.get<HowItWorksStep[]>(this.apiUrl);
}
}