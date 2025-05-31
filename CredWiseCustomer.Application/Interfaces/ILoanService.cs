using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface ILoanService
    {
        Task<int> ApplyForLoanAsync(ApplyLoanDto dto);
        Task<LoanStatusDto?> GetLoanStatusAsync(int loanApplicationId);
        Task<IEnumerable<LoanStatusDto>> GetAllLoansForUserAsync(int userId);
        Task<IEnumerable<LoanStatusDto>> GetAllLoanApplicationsAsync();
        Task<IEnumerable<LoanProductDocumentDto>> GetRequiredDocumentsAsync(int loanProductId);
        Task<bool> UploadLoanProductDocumentAsync(int loanProductId, int? loanApplicationId, string documentName, byte[] documentContent, string createdBy);
    }
} 