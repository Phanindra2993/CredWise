import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { FDCalculation, FDType, FDStatus } from '../_models/fd.model';

@Injectable({
  providedIn: 'root'
})
export class FdService {
  private FDTypesUrl = 'https://localhost:7001/api/FDType';
  private FdApplicationsUrl = 'https://localhost:7001/api/FDApplication';
  private FdStatusByUserIdUrl = 'https://localhost:7037/api/Fd/user';
  private FileUploadUrl = 'https://localhost:7037/api/Loan/upload-loan-product-document';
  constructor(private http:HttpClient) { } 

  //submit fd application 
  submitFDApplication(application:{ email:string, fdTypeId:number, amount:number }):Observable<any>{
      return this.http.post<any>(`${this.FdApplicationsUrl}`,application);
  }
  //getting fd types 
  getFdTypes():Observable<FDType[]>{
    return this.http.get<FDType[]>(`${this.FDTypesUrl}`);
  }

  //file upload 
  uploadFdDocument(file: File, fdTypeId: number, fdApplicationId: number, documentName: string) {
    const formData = new FormData();
    formData.append('File', file);
    formData.append('LoanProductId', fdTypeId.toString());
    formData.append('LoanApplicationId', fdApplicationId?.toString() || '');
    formData.append('DocumentName', documentName);

    return this.http.post(this.FileUploadUrl, formData);
  } 
  
  //fd status page 
  getFdStatusByUserId(userId: number): Observable<FDStatus[]> {
    return this.http.get<FDStatus[]>(`${this.FdStatusByUserIdUrl}/${userId}`);
  }

}
