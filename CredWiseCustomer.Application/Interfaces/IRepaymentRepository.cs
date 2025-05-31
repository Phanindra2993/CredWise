using CredWiseCustomer.Core.Entities;
using CredWiseCustomer.Application.DTOs;

namespace CredWiseCustomer.Application.Interfaces
{
    public interface IRepaymentRepository
    {
        Task<IEnumerable<LoanRepaymentSchedule>> GetRepaymentScheduleAsync(int loanApplicationId);
        Task<LoanRepaymentSchedule?> GetRepaymentByIdAsync(int repaymentId);
        Task UpdateRepaymentAsync(LoanRepaymentSchedule repayment);
        Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryByUserIdAsync(int userId);
        Task AddPaymentTransactionAsync(PaymentTransaction transaction);
    }
} 