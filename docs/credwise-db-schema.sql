-- USERS
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Password NVARCHAR(256) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Admin', 'Customer')),
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100),
    ModifiedAt DATETIME,
    ModifiedBy NVARCHAR(100),
    IsActive BIT DEFAULT 1
);

-- LOAN PRODUCTS (BASE)
CREATE TABLE LoanProducts (
    LoanProductId INT PRIMARY KEY IDENTITY(1,1),
    ImageUrl NVARCHAR(MAX) NOT NULL,
    Title NVARCHAR(150) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    MaxLoanAmount DECIMAL(18,2) NOT NULL,
    LoanType NVARCHAR(20) NOT NULL CHECK (LoanType IN ('HOME', 'PERSONAL', 'GOLD')),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL
);

-- HOME LOAN DETAILS
CREATE TABLE HomeLoanDetails (
    LoanProductId INT PRIMARY KEY,
    InterestRate DECIMAL(5,2) NOT NULL,
    TenureMonths INT NOT NULL,
    ProcessingFee DECIMAL(18,2) NOT NULL,
    DownPaymentPercentage DECIMAL(5,2) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanProductId) REFERENCES LoanProducts(LoanProductId)
);

-- PERSONAL LOAN DETAILS
CREATE TABLE PersonalLoanDetails (
    LoanProductId INT PRIMARY KEY,
    InterestRate DECIMAL(5,2) NOT NULL,
    TenureMonths INT NOT NULL,
    ProcessingFee DECIMAL(18,2) NOT NULL,
    MinSalaryRequired DECIMAL(18,2) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanProductId) REFERENCES LoanProducts(LoanProductId)
);

-- GOLD LOAN DETAILS
CREATE TABLE GoldLoanDetails (
    LoanProductId INT PRIMARY KEY,
    InterestRate DECIMAL(5,2) NOT NULL,
    TenureMonths INT NOT NULL,
    ProcessingFee DECIMAL(18,2) NOT NULL,
    GoldPurityRequired NVARCHAR(20) NOT NULL,
    RepaymentType NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanProductId) REFERENCES LoanProducts(LoanProductId)
);

-- LOAN PRODUCT DOCUMENTS (PDFs, Brochures, etc.)
CREATE TABLE LoanProductDocuments (
    LoanProductDocumentId INT PRIMARY KEY IDENTITY(1,1),
    LoanProductId INT NOT NULL,
	-- income
	-- EmploymentType
    DocumentName NVARCHAR(100) NOT NULL,
    DocumentContent VARBINARY(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanProductId) REFERENCES LoanProducts(LoanProductId)
);

-- LOAN APPLICATIONS
CREATE TABLE LoanApplications (
    LoanApplicationId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    LoanProductId INT NOT NULL,
    RequestedAmount DECIMAL(18,2) NOT NULL,
    RequestedTenure INT NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN (
        'Initial Review', 'In Processing', 'Documents Collected',
        'Decision Pending', 'Approved', 'Rejected'
    )),
    DecisionDate DATETIME NULL,
    DecisionReason NVARCHAR(500) NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100),
    ModifiedAt DATETIME,
    ModifiedBy NVARCHAR(100),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (LoanProductId) REFERENCES LoanProducts(LoanProductId)
);

-- LOAN BANK STATEMENTS (Customer uploads)
CREATE TABLE LoanBankStatements (
    BankStatementId INT PRIMARY KEY IDENTITY(1,1),
    LoanApplicationId INT NOT NULL,
    DocumentName NVARCHAR(100) NOT NULL,
    DocumentPath NVARCHAR(500) NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Pending', 'Verified', 'Rejected')) DEFAULT 'Pending',
    RejectionReason NVARCHAR(255) NULL,
    VerifiedBy INT NULL,
    VerifiedAt DATETIME NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanApplicationId) REFERENCES LoanApplications(LoanApplicationId),
    FOREIGN KEY (VerifiedBy) REFERENCES Users(UserId)
);

-- LOAN REPAYMENT SCHEDULE
CREATE TABLE LoanRepaymentSchedule (
    RepaymentId INT PRIMARY KEY IDENTITY(1,1),
    LoanApplicationId INT NOT NULL,
    InstallmentNumber INT NOT NULL,
    DueDate DATE NOT NULL,
    PrincipalAmount DECIMAL(18,2) NOT NULL,
    InterestAmount DECIMAL(18,2) NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Pending', 'Paid', 'Overdue')) DEFAULT 'Pending',
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanApplicationId) REFERENCES LoanApplications(LoanApplicationId)
);

-- PAYMENT TRANSACTIONS (Loan repayments)
CREATE TABLE PaymentTransactions (
    TransactionId INT PRIMARY KEY IDENTITY(1,1),
    LoanApplicationId INT NOT NULL,
    RepaymentId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) NOT NULL,
    TransactionStatus NVARCHAR(20) NOT NULL CHECK (TransactionStatus IN ('Success', 'Failed', 'Pending')),
    TransactionReference NVARCHAR(100) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanApplicationId) REFERENCES LoanApplications(LoanApplicationId),
    FOREIGN KEY (RepaymentId) REFERENCES LoanRepaymentSchedule(RepaymentId)
);

-- FD TYPES
CREATE TABLE FDTypes (
    FDTypeId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500) NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    MinAmount DECIMAL(18,2) NOT NULL,
    MaxAmount DECIMAL(18,2) NOT NULL,
    Duration INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL
);

-- FD APPLICATIONS
CREATE TABLE FDApplications (
    FDApplicationId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    FDTypeId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Duration INT NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN ('Pending', 'Active', 'Matured', 'Closed', 'Rejected')),
    MaturityDate DATETIME NULL,
    MaturityAmount DECIMAL(18,2) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (FDTypeId) REFERENCES FDTypes(FDTypeId)
);

-- FD TRANSACTIONS (FD payments, payouts, withdrawals)
CREATE TABLE FDTransactions (
    FDTransactionId INT PRIMARY KEY IDENTITY(1,1),
    FDApplicationId INT NOT NULL,
    TransactionType NVARCHAR(20) NOT NULL CHECK (TransactionType IN ('Deposit', 'InterestPayout', 'MaturityPayout', 'PrematureWithdrawal', 'Refund')),
    Amount DECIMAL(18,2) NOT NULL,
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) NOT NULL,
    TransactionStatus NVARCHAR(20) NOT NULL CHECK (TransactionStatus IN ('Success', 'Failed', 'Pending')),
    TransactionReference NVARCHAR(100) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (FDApplicationId) REFERENCES FDApplications(FDApplicationId)
);

-- DECISION APP LOGS
CREATE TABLE DecisionAppLogs (
    LogId INT PRIMARY KEY IDENTITY(1,1),
    LoanApplicationId INT NOT NULL,
    DecisionInput NVARCHAR(MAX) NULL,
    DecisionOutput NVARCHAR(MAX) NULL,
    ProcessedAt DATETIME DEFAULT GETDATE(),
    ProcessingTime INT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedBy NVARCHAR(100) NOT NULL,
    ModifiedAt DATETIME NULL,
    ModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (LoanApplicationId) REFERENCES LoanApplications(LoanApplicationId)
);

-- AUDIT LOGS
 --CREATE TABLE AuditLogs (
 --   AuditId INT PRIMARY KEY IDENTITY(1,1),
   -- UserId INT NOT NULL,
   -- Action NVARCHAR(100) NOT NULL,
    --EntityType NVARCHAR(50) NOT NULL,
    --EntityId NVARCHAR(50) NOT NULL,
    --OldValue NVARCHAR(MAX) NULL,
    --NewValue NVARCHAR(MAX) NULL,
   -- LoggedAt DATETIME DEFAULT GETDATE(),
   -- IpAddress NVARCHAR(50) NULL,
  --  IsActive BIT NOT NULL DEFAULT 1,
   -- CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    --CreatedBy NVARCHAR(100) NOT NULL,
   -- ModifiedAt DATETIME NULL,
    --ModifiedBy NVARCHAR(100) NULL,
    --FOREIGN KEY (UserId) REFERENCES Users(UserId)
--);
