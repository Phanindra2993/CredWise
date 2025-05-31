using CredWiseCustomer.Application.DTOs;

namespace CredWiseCustomer.Application.Interfaces;

public interface ILoanApplicationService
{
    Task<LoanApplicationResponseDto> ApplyForGoldLoanAsync(GoldLoanApplicationDto application);
    Task<LoanApplicationResponseDto> ApplyForHomeLoanAsync(HomeLoanApplicationDto application);
    Task<LoanApplicationResponseDto> ApplyForPersonalLoanAsync(PersonalLoanApplicationDto application);
    Task<LoanApplicationResponseDto> GetLoanApplicationStatusAsync(int loanApplicationId);
    Task<IEnumerable<LoanApplicationResponseDto>> GetUserLoanApplicationsAsync(int userId);
    Task<IEnumerable<LoanApplicationResponseDto>> GetAllLoanApplicationsAsync();
} 