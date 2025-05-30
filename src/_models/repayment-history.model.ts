export interface RepaymentHistory {
    transactionId: number;
    loanApplicationId: number;
    repaymentId: number;
    amount: number;
    paymentDate: string;
    paymentMethod: string;
    status: string;
    reference: string;
  }