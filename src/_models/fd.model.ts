export interface FDType {
    fdTypeId: number;
    name: string;
    description: string;
    interestRate: number;
    minAmount: number;
    maxAmount: number;
    duration: number;
  }
  
  export interface FDCalculation {
    interestRate: number;
    maturityAmount: number;
    maturityDate: Date;
  }

  export interface FDStatus {
    
    amount: number;
    duration: number;
    interestRate: number;
    status: string;
    maturityDate: Date;
    maturityAmount: number;
    fdTypeName: string;
    createdAt: Date;  
    userId: number;
    fdTypeId: number;
    fdApplicationId: number;
    isActive: boolean;
    createdBy: string;

  }