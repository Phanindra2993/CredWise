using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CredWiseCustomer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoanApplicationController : ControllerBase
{
    private readonly ILoanApplicationService _loanApplicationService;

    public LoanApplicationController(ILoanApplicationService loanApplicationService)
    {
        _loanApplicationService = loanApplicationService;
    }

    [HttpPost("gold")]
    public async Task<ActionResult<LoanApplicationResponseDto>> ApplyForGoldLoan([FromBody] GoldLoanApplicationDto application)
    {
        try
        {
            var result = await _loanApplicationService.ApplyForGoldLoanAsync(application);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("home")]
    public async Task<ActionResult<LoanApplicationResponseDto>> ApplyForHomeLoan([FromBody] HomeLoanApplicationDto application)
    {
        try
        {
            var result = await _loanApplicationService.ApplyForHomeLoanAsync(application);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("personal")]
    public async Task<ActionResult<LoanApplicationResponseDto>> ApplyForPersonalLoan([FromBody] PersonalLoanApplicationDto application)
    {
        try
        {
            var result = await _loanApplicationService.ApplyForPersonalLoanAsync(application);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{loanApplicationId}")]
    public async Task<ActionResult<LoanApplicationResponseDto>> GetLoanApplicationStatus(int loanApplicationId)
    {
        try
        {
            var result = await _loanApplicationService.GetLoanApplicationStatusAsync(loanApplicationId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<LoanApplicationResponseDto>>> GetUserLoanApplications(int userId)
    {
        var result = await _loanApplicationService.GetUserLoanApplicationsAsync(userId);
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<LoanApplicationResponseDto>>> GetAllLoanApplications()
    {
        var result = await _loanApplicationService.GetAllLoanApplicationsAsync();
        return Ok(result);
    }
} 