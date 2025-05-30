export interface HomeLoanRequest {
  userId: number;
  loanProductId: number;
  requestedAmount: number;
  requestedTenure: number;
  gender: string;
  dob: string;
  aadhaar: string;
  address: string;
  income: number;
  employmentType: string;
  createdBy: string;
  propertyAddress: string;
  downPaymentPercentage: number;
}

export interface GoldLoanRequest {
  userId: number;
  loanProductId: number;
  requestedAmount: number;
  requestedTenure: number;
  gender: string;
  dob: string;
  aadhaar: string;
  address: string;
  income: number;
  employmentType: string;
  createdBy: string;
  goldWeight: number;
  goldPurity: string;
}

export interface PersonalLoanRequest {
  userId: number;
  loanProductId: number;
  requestedAmount: number;
  requestedTenure: number;
  gender: string;
  dob: string;
  aadhaar: string;
  address: string;
  income: number;
  employmentType: string;
  createdBy: string;
}
