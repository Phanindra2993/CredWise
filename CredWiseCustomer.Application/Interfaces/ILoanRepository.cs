using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task<int> AddLoanApplicationAsync(LoanApplication loan);
        Task<LoanApplication?> GetLoanApplicationByIdAsync(int loanApplicationId);
        Task<IEnumerable<LoanApplication>> GetLoansByUserIdAsync(int userId);
        Task<IEnumerable<LoanApplication>> GetAllLoanApplicationsAsync();
        Task<LoanProduct> GetLoanProductByIdAsync(int loanProductId);
        Task AddGoldLoanDetailAsync(GoldLoanDetail detail);
        Task AddHomeLoanDetailAsync(HomeLoanDetail detail);
        Task AddPersonalLoanDetailAsync(PersonalLoanDetail detail);
        Task<IEnumerable<LoanProductDocument>> GetRequiredDocumentsAsync(int loanProductId);
        Task AddLoanBankStatementAsync(LoanBankStatement statement);
        Task AddLoanProductDocumentAsync(LoanProductDocument doc);
    }
} 