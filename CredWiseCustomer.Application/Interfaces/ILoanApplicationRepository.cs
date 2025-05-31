using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Interfaces;

public interface ILoanApplicationRepository
{
    Task<LoanApplication> GetByIdAsync(int id);
    Task<IEnumerable<LoanApplication>> GetByUserIdAsync(int userId);
    Task<LoanApplication> AddAsync(LoanApplication loanApplication);
    Task<GoldLoanApplication> AddGoldLoanApplicationAsync(GoldLoanApplication goldLoanApplication);
    Task<HomeLoanApplication> AddHomeLoanApplicationAsync(HomeLoanApplication homeLoanApplication);
    Task<LoanProduct> GetLoanProductByIdAsync(int id);
    Task<bool> UpdateAsync(LoanApplication loanApplication);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<LoanApplication>> GetAllAsync();
} 