using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CredWiseCustomer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // POST: api/Loan
        //[HttpPost]
        //public async Task<ActionResult<ApiResponse<LoanStatusDto>>> ApplyForLoan([FromBody] ApplyLoanDto dto)
        //{
        //    dto.CreatedBy = "Customer";
        //    var loanId = await _loanService.ApplyForLoanAsync(dto);
        //    var loanStatus = await _loanService.GetLoanStatusAsync(loanId);
        //    if (loanStatus == null)
        //        return NotFound(ApiResponse<object>.CreateError("Loan application not found"));
        //    return CreatedAtAction(nameof(GetLoanStatus), new { loanApplicationId = loanId }, ApiResponse<LoanStatusDto>.CreateSuccess(loanStatus, "Loan application submitted successfully"));
        //}

        // GET: api/Loan/{loanApplicationId}
        //[HttpGet("{loanApplicationId:int}")]
        //public async Task<ActionResult<ApiResponse<LoanStatusDto>>> GetLoanStatus(int loanApplicationId)
        //{
        //    var status = await _loanService.GetLoanStatusAsync(loanApplicationId);
        //    if (status == null)
        //        return NotFound(ApiResponse<object>.CreateError($"Loan application with ID {loanApplicationId} not found"));
        //    return Ok(ApiResponse<LoanStatusDto>.CreateSuccess(status, "Loan status retrieved successfully"));
        //}

        // GET: api/Loan/user/{userId}
        //[HttpGet("user/{userId:int}")]
        //public async Task<ActionResult<ApiResponse<IEnumerable<LoanStatusDto>>>> GetAllLoansForUser(int userId)
        //{
        //    var loans = await _loanService.GetAllLoansForUserAsync(userId);
        //    return Ok(ApiResponse<IEnumerable<LoanStatusDto>>.CreateSuccess(loans, "Loans retrieved successfully"));
        //}

        // GET: api/Loan/all
        //[HttpGet("all")]
        //public async Task<ActionResult<ApiResponse<IEnumerable<LoanStatusDto>>>> GetAllLoanApplications()
        //{
        //    var loans = await _loanService.GetAllLoanApplicationsAsync();
        //    return Ok(ApiResponse<IEnumerable<LoanStatusDto>>.CreateSuccess(loans, "All loan applications retrieved successfully"));
        //}

        [HttpPost("upload-loan-product-document")]
        public async Task<ActionResult<ApiResponse<object>>> UploadLoanProductDocument([FromForm] UploadLoanProductDocumentDto dto)
        {
            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);
            var fileBytes = ms.ToArray();

            var result = await _loanService.UploadLoanProductDocumentAsync(
                dto.LoanProductId,
                dto.LoanApplicationId,
                dto.DocumentName,
                fileBytes,
                "Customer"
            );
            
            if (result)
                return Ok(ApiResponse<object>.CreateSuccess(null, "Document uploaded successfully"));
            return BadRequest(ApiResponse<object>.CreateError("Upload failed"));
        }
    }
} 