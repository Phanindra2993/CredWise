using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CredWiseCustomer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FdController : ControllerBase
    {
        private readonly IFdService _fdService;

        public FdController(IFdService fdService)
        {
            _fdService = fdService;
        }

        // POST: api/Fd
        [HttpPost]
        public async Task<ActionResult<FdStatusDto>> ApplyForFd([FromBody] ApplyFdDto dto)
        {
            dto.CreatedBy = "Customer";
            var fdId = await _fdService.ApplyForFdAsync(dto);
            var fdStatus = await _fdService.GetFdStatusAsync(fdId);
            return CreatedAtAction(nameof(GetFdStatus), new { fdApplicationId = fdId }, fdStatus);
        }

        // GET: api/Fd/{fdApplicationId}
        [HttpGet("{fdApplicationId:int}")]
        public async Task<ActionResult<FdStatusDto>> GetFdStatus(int fdApplicationId)
        {
            var status = await _fdService.GetFdStatusAsync(fdApplicationId);
            if (status == null)
                return NotFound();
            return Ok(status);
        }

        // GET: api/Fd/user/{userId}
        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<IEnumerable<FdStatusDto>>> GetAllFdsForUser(int userId)
        {
            var fds = await _fdService.GetAllFdsForUserAsync(userId);
            return Ok(fds);
        }

        // GET: api/Fd/payment-schedule/{fdApplicationId}
        [HttpGet("payment-schedule/{fdApplicationId:int}")]
        public async Task<ActionResult<IEnumerable<FdPaymentScheduleDto>>> GetFdPaymentSchedule(int fdApplicationId)
        {
            var schedule = await _fdService.GetFdPaymentScheduleAsync(fdApplicationId);
            return Ok(schedule);
        }
    }
}
