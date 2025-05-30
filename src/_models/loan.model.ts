export interface LoanDetail {
  interestRate: number;
  tenureMonths: number;
  processingFee: number;
  downPaymentPercentage?: number;
  minSalaryRequired?: number;
  goldPurityRequired?: string;
  repaymentType: string;
  documentsRequired: string[];
}

export interface Loan {
  id: number;
  image: string;
  title: string;
  description: string;
  maxLoanAmount: number;
  loanType: 'HOME' | 'PERSONAL' | 'GOLD';
  loanDetail: LoanDetail;
}

export interface LoanResponse {
  status: string;
  data: Loan[];
  message: string;
}