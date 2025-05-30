import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Loan, LoanResponse } from '../_models/loan.model';
import { PersonalLoanRequest, GoldLoanRequest, HomeLoanRequest } from '../_models/loan-application.model';

@Injectable({
  providedIn: 'root',
})
export class LoanService {
  private baseUrl = 'https://localhost:7001/api/LoanProduct';
  private emiUrl = 'https://localhost:7037/api/Calculator/returns'

  private uploadUrl = 'https://localhost:7037/api/Loan/upload-loan-product-document';
  private personalLoanUrl = 'https://localhost:7037/api/LoanApplication/personal';
  private goldLoanUrl = 'https://localhost:7037/api/LoanApplication/gold';
  private homeLoanUrl = 'https://localhost:7037/api/LoanApplication/home';
  private loanStatusUrl = 'https://localhost:7037/api/LoanApplication/user';
  private genderUrl = 'https://localhost:7037/genders';
  private employmentTypeUrl = 'https://localhost:7037/employment-types';

    

  constructor(private http: HttpClient) {}
// all loans
  getLoanProducts(): Observable<Loan[]> {
    return this.http.get<LoanResponse>(`${this.baseUrl}`).pipe(
      tap(response => console.log('Raw API Response:', response)),
      map(response => {
        if (!response) {
          console.error('Invalid API response:', response);
          return [];
        }
        // Convert string IDs to numbers
        return response.data.map(loan => ({
          ...loan,
          id: Number(loan.id)
        }));
      })
    );
  }  
// specific loan details page 
  getLoanProductById(id: number): Observable<Loan | undefined> {
    return this.getLoanProducts().pipe(
      tap(loans => console.log('All loans:', loans)),
      map((loans: Loan[]) => {
        const found = loans.find((loan) => loan.id === id);
     //   console.log('Found loan:', found);
        return found;
      })
    );
  } 

  getLoanProductByPurpose(purpose:string):Observable<Loan[]>{
    return this.getLoanProducts().pipe(
      map((loans:Loan[])=>loans.filter((loan:Loan)=>loan.loanType===purpose))
    )
  }
  
  // submitLoanApplication(application: LoanApplication): Observable<any> {
  //   return this.http.post(`${this.uploadUrl}`, application);
  // }
//loan status
  getUserLoanApplicationsByUserId(userId: string): Observable<any[]>{
    return this.http.get<any[]>(`${this.loanStatusUrl}/${userId}`);
  }

  getGenderOptions(): Observable<{ value: string, label: string }[]> {
    return this.http.get<{ value: string, label: string }[]>(`${this.genderUrl}`);
  }

  getEmploymentTypeOptions(): Observable<{ value: string, label: string }[]> {
    return this.http.get<{ value: string, label: string }[]>(`${this.employmentTypeUrl}`);
  }
  calculateEMI(principal  : number, annualInterestRate: number, loanTermMonths: number): Observable<number> {
    const url = `${this.emiUrl}`;
    const body = { principal, annualInterestRate, loanTermMonths };
    return this.http.post<number>(url, body);
  }

  uploadPersonalLoanFile(file: File, loanProductId: number, loanApplicationId: number, documentName: string) {
    const formData = new FormData();
    formData.append('file', file);
    formData.append('loanProductId', loanProductId.toString());
    formData.append('loanApplicationId', loanApplicationId.toString());
    formData.append('documentName', documentName);

    return this.http.post<{ success: boolean, data: any, message: string, errors: any }>(
      `${this.uploadUrl}`,
      formData,
      {
        headers: {
          // Don't set Content-Type header - browser will set it automatically with boundary
          'Accept': 'application/json'
        }
      }
    );
  }

  //personal loan API 
  applyPersonalLoan(application: PersonalLoanRequest): Observable<any> {
    return this.http.post(`${this.personalLoanUrl}`, application);
  }

  // Gold loan API
  applyGoldLoan(application: GoldLoanRequest): Observable<any> {
    return this.http.post(`${this.goldLoanUrl}`, application);
  }

  // Home loan API
  applyHomeLoan(application: HomeLoanRequest): Observable<any> {
    return this.http.post(`${this.homeLoanUrl}`, application);
  }
}
