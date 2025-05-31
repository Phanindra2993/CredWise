using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CredWiseCustomer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanEnquiryController : ControllerBase
    {
        private readonly ILoanEnquiryService _service;

        public LoanEnquiryController(ILoanEnquiryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<LoanEnquiryResponseDto>> AddEnquiry([FromBody] LoanEnquiryRequestDto dto)
        {
            var result = await _service.AddEnquiryAsync(dto);
            return Ok(result);
        }
    }
} 