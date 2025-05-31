using AutoMapper;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using CredWiseCustomer.Core.Entities;

namespace CredWiseCustomer.Application.Services
{
    public class RepaymentService : IRepaymentService
    {
        private readonly IRepaymentRepository _repo;
        private readonly IMapper _mapper;

        public RepaymentService(IRepaymentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RepaymentScheduleDto>> GetRepaymentScheduleAsync(int loanApplicationId)
        {
            var schedule = await _repo.GetRepaymentScheduleAsync(loanApplicationId);
            return schedule.Select(_mapper.Map<RepaymentScheduleDto>);
        }

        public async Task<bool> SubmitPaymentAsync(SubmitPaymentDto dto)
        {
            var repayment = await _repo.GetRepaymentByIdAsync(dto.RepaymentId);
            if (repayment == null || repayment.Status == "Paid")
                return false;

            if (repayment.TotalAmount != dto.Amount)
                return false;

            repayment.Status = "Paid";
            await _repo.UpdateRepaymentAsync(repayment);

            // Create and store the payment transaction
            var paymentTransaction = new PaymentTransaction
            {
                LoanApplicationId = repayment.LoanApplicationId,
                RepaymentId = repayment.RepaymentId,
                Amount = dto.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                TransactionStatus = "Success",
                TransactionReference = Guid.NewGuid().ToString(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };
            // Save the payment transaction (assume _repo or context has AddPaymentTransactionAsync)
            await _repo.AddPaymentTransactionAsync(paymentTransaction);

            return true;
        }

        public async Task<IEnumerable<PaymentHistoryDto>> GetPaymentHistoryByUserIdAsync(int userId)
        {
            return await _repo.GetPaymentHistoryByUserIdAsync(userId);
        }
    }
} 