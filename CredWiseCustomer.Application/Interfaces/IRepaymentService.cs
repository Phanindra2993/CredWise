using CredWiseCustomer.Application.DTOs;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IRepaymentService
    {
        Task<IEnumerable<RepaymentScheduleDto>> GetRepaymentScheduleAsync(int loanApplicationId);
        Task<bool> SubmitPaymentAsync(SubmitPaymentDto dto);
        Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryByUserIdAsync(int userId);
    }
} 