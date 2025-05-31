using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CredWiseCustomer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RepaymentController : ControllerBase
    {
        private readonly IRepaymentService _service;

        public RepaymentController(IRepaymentService service)
        {
            _service = service;
        }

        // GET: api/Repayment/schedule/{loanApplicationId}
        [HttpGet("schedule/{loanApplicationId:int}")]
        public async Task<ActionResult<IEnumerable<RepaymentScheduleDto>>> GetSchedule(int loanApplicationId)
        {
            var schedule = await _service.GetRepaymentScheduleAsync(loanApplicationId);
            return Ok(schedule);
        }

        // POST: api/Repayment/pay
        [HttpPost("pay")]
        public async Task<IActionResult> SubmitPayment([FromBody] SubmitPaymentDto dto)
        {
            var result = await _service.SubmitPaymentAsync(dto);
            if (!result)
                return BadRequest("Payment amount does not match due amount or already paid.");
            // Fetch the updated repayment schedule to get payment type
            var schedule = await _service.GetRepaymentScheduleAsync(dto.RepaymentId);
            var paidInstallment = schedule.FirstOrDefault(x => x.RepaymentId == dto.RepaymentId);
            return Ok(new { message = "Payment successful.", paymentType = paidInstallment?.PaymentType });
        }

        [HttpGet("user/{userId}/payment-history")]
        public async Task<ActionResult<IEnumerable<PaymentHistoryDto>>> GetPaymentHistoryByUser(int userId)
        {
            var history = await _service.GetPaymentHistoryByUserIdAsync(userId);
            return Ok(history);
        }
    }
} 