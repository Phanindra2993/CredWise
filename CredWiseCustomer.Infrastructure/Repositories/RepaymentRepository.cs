using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;
using CredWiseCustomer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CredWiseCustomer.Application.DTOs;

namespace CredWiseCustomer.Infrastructure.Repositories
{
    public class RepaymentRepository : IRepaymentRepository
    {
        private readonly AppDbContext _context;
        public RepaymentRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<LoanRepaymentSchedule>> GetRepaymentScheduleAsync(int loanApplicationId)
        {
            return await _context.LoanRepaymentSchedules
                .Where(r => r.LoanApplicationId == loanApplicationId)
                .ToListAsync();
        }

        public async Task<LoanRepaymentSchedule?> GetRepaymentByIdAsync(int repaymentId)
        {
            return await _context.LoanRepaymentSchedules.FindAsync(repaymentId);
        }

        public async Task UpdateRepaymentAsync(LoanRepaymentSchedule repayment)
        {
            _context.LoanRepaymentSchedules.Update(repayment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryByUserIdAsync(int userId)
        {
            var result = await (from pt in _context.PaymentTransactions
                                join la in _context.LoanApplications on pt.LoanApplicationId equals la.LoanApplicationId
                                where la.UserId == userId
                                orderby pt.PaymentDate descending
                                select new PaymentHistoryDto
                                {
                                    TransactionId = pt.TransactionId,
                                    LoanApplicationId = pt.LoanApplicationId,
                                    RepaymentId = pt.RepaymentId,
                                    Amount = pt.Amount,
                                    PaymentDate = pt.PaymentDate,
                                    PaymentMethod = pt.PaymentMethod,
                                    Status = pt.TransactionStatus,
                                    Reference = pt.TransactionReference
                                }).ToListAsync();
            return result;
        }

        public async Task AddPaymentTransactionAsync(PaymentTransaction transaction)
        {
            _context.PaymentTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
} 