USE [master]
GO
/****** Object:  Database [CredWiseDB]    Script Date: 5/24/2025 2:56:04 PM ******/
CREATE DATABASE [CredWiseDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CredWiseDB', FILENAME = N'C:\Users\Sai Sahith Ajju\CredWiseDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CredWiseDB_log', FILENAME = N'C:\Users\Sai Sahith Ajju\CredWiseDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CredWiseDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CredWiseDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CredWiseDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CredWiseDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CredWiseDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CredWiseDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CredWiseDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CredWiseDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CredWiseDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CredWiseDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CredWiseDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CredWiseDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CredWiseDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CredWiseDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CredWiseDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CredWiseDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CredWiseDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CredWiseDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CredWiseDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CredWiseDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CredWiseDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CredWiseDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CredWiseDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CredWiseDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CredWiseDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CredWiseDB] SET  MULTI_USER 
GO
ALTER DATABASE [CredWiseDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CredWiseDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CredWiseDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CredWiseDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CredWiseDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CredWiseDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CredWiseDB] SET QUERY_STORE = OFF
GO
USE [CredWiseDB]
GO
/****** Object:  User [root]    Script Date: 5/24/2025 2:56:04 PM ******/
CREATE USER [root] FOR LOGIN [root] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[DecisionAppLogs]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DecisionAppLogs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[DecisionInput] [nvarchar](max) NULL,
	[DecisionOutput] [nvarchar](max) NULL,
	[ProcessedAt] [datetime] NULL,
	[ProcessingTime] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDApplications]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDApplications](
	[FDApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FDTypeId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Duration] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[MaturityDate] [datetime] NULL,
	[MaturityAmount] [decimal](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDTransactions]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDTransactions](
	[FDTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[FDApplicationId] [int] NOT NULL,
	[TransactionType] [nvarchar](20) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[TransactionStatus] [nvarchar](20) NOT NULL,
	[TransactionReference] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDTransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FDTypes]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FDTypes](
	[FDTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[MinAmount] [decimal](18, 2) NOT NULL,
	[MaxAmount] [decimal](18, 2) NOT NULL,
	[Duration] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[FDTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GoldLoanDetails]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GoldLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[GoldPurityRequired] [nvarchar](20) NOT NULL,
	[RepaymentType] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HomeLoanDetails]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HomeLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[DownPaymentPercentage] [decimal](5, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanApplications]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanApplications](
	[LoanApplicationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[DOB] [date] NOT NULL,
	[Aadhaar] [nvarchar](12) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[Income] [decimal](18, 2) NOT NULL,
	[EmploymentType] [nvarchar](50) NOT NULL,
	[LoanProductId] [int] NOT NULL,
	[RequestedAmount] [decimal](18, 2) NOT NULL,
	[RequestedTenure] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[DecisionDate] [datetime] NULL,
	[DecisionReason] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanBankStatements]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanBankStatements](
	[BankStatementId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[DocumentPath] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[RejectionReason] [nvarchar](255) NULL,
	[VerifiedBy] [int] NULL,
	[VerifiedAt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[BankStatementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanEnquiries]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanEnquiries](
	[EnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[LoanAmountRequired] [decimal](18, 2) NOT NULL,
	[LoanPurpose] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanProductDocuments]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanProductDocuments](
	[LoanProductDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[LoanProductId] [int] NOT NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[DocumentContent] [varbinary](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanProducts]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanProducts](
	[LoanProductId] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[MaxLoanAmount] [decimal](18, 2) NOT NULL,
	[LoanType] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanRepaymentSchedule]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanRepaymentSchedule](
	[RepaymentId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[InstallmentNumber] [int] NOT NULL,
	[DueDate] [date] NOT NULL,
	[PrincipalAmount] [decimal](18, 2) NOT NULL,
	[InterestAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[RepaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTransactions]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTransactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[LoanApplicationId] [int] NOT NULL,
	[RepaymentId] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[TransactionStatus] [nvarchar](20) NOT NULL,
	[TransactionReference] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonalLoanDetails]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonalLoanDetails](
	[LoanProductId] [int] NOT NULL,
	[InterestRate] [decimal](5, 2) NOT NULL,
	[TenureMonths] [int] NOT NULL,
	[ProcessingFee] [decimal](18, 2) NOT NULL,
	[MinSalaryRequired] [decimal](18, 2) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/24/2025 2:56:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](15) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Password], [Email], [FirstName], [LastName], [PhoneNumber], [Role], [CreatedAt], [CreatedBy], [ModifiedAt], [ModifiedBy], [IsActive]) VALUES (1, N'$2a$12$m9VUuQzJY3VvjW3Q4XHrE.5dD1q2sKbLx0yZ1NcRtBwSfGpYhHvC2', N'admin@credwise.com', N'System', N'Administrator', N'+1234567890', N'Admin', CAST(N'2025-05-24T11:13:57.340' AS DateTime), N'System', NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__LoanAppl__C4B33369E6CE9FEF]    Script Date: 5/24/2025 2:56:05 PM ******/
ALTER TABLE [dbo].[LoanApplications] ADD UNIQUE NONCLUSTERED 
(
	[Aadhaar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053421165417]    Script Date: 5/24/2025 2:56:05 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT (getdate()) FOR [ProcessedAt]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DecisionAppLogs] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDApplications] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDApplications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT (getdate()) FOR [TransactionDate]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDTransactions] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FDTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FDTypes] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[GoldLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[GoldLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[HomeLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[HomeLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanApplications] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanApplications] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanBankStatements] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanEnquiries] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanProductDocuments] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanProductDocuments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanProducts] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanProducts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PaymentTransactions] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[PersonalLoanDetails] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PersonalLoanDetails] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DecisionAppLogs]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD FOREIGN KEY([FDTypeId])
REFERENCES [dbo].[FDTypes] ([FDTypeId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD FOREIGN KEY([FDApplicationId])
REFERENCES [dbo].[FDApplications] ([FDApplicationId])
GO
ALTER TABLE [dbo].[GoldLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[HomeLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD FOREIGN KEY([VerifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LoanProductDocuments]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD FOREIGN KEY([LoanApplicationId])
REFERENCES [dbo].[LoanApplications] ([LoanApplicationId])
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD FOREIGN KEY([RepaymentId])
REFERENCES [dbo].[LoanRepaymentSchedule] ([RepaymentId])
GO
ALTER TABLE [dbo].[PersonalLoanDetails]  WITH CHECK ADD FOREIGN KEY([LoanProductId])
REFERENCES [dbo].[LoanProducts] ([LoanProductId])
GO
ALTER TABLE [dbo].[FDApplications]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Closed' OR [Status]='Matured' OR [Status]='Active' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD CHECK  (([TransactionType]='Refund' OR [TransactionType]='PrematureWithdrawal' OR [TransactionType]='MaturityPayout' OR [TransactionType]='InterestPayout' OR [TransactionType]='Deposit'))
GO
ALTER TABLE [dbo].[FDTransactions]  WITH CHECK ADD CHECK  (([TransactionStatus]='Pending' OR [TransactionStatus]='Failed' OR [TransactionStatus]='Success'))
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD CHECK  (([EmploymentType]='Salaried' OR [EmploymentType]='Self-Employed'))
GO
ALTER TABLE [dbo].[LoanApplications]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Approved' OR [Status]='Decision Pending' OR [Status]='Documents Collected' OR [Status]='In Processing' OR [Status]='Initial Review'))
GO
ALTER TABLE [dbo].[LoanBankStatements]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Verified' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[LoanProducts]  WITH CHECK ADD CHECK  (([LoanType]='GOLD' OR [LoanType]='PERSONAL' OR [LoanType]='HOME'))
GO
ALTER TABLE [dbo].[LoanRepaymentSchedule]  WITH CHECK ADD CHECK  (([Status]='Overdue' OR [Status]='Paid' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD CHECK  (([TransactionStatus]='Pending' OR [TransactionStatus]='Failed' OR [TransactionStatus]='Success'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Customer' OR [Role]='Admin'))
GO
USE [master]
GO
ALTER DATABASE [CredWiseDB] SET  READ_WRITE 
GO


sele