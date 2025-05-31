using Microsoft.AspNetCore.Mvc;
using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly IReturnsCalculatorService _returnsCalculatorService;

    public CalculatorController(IReturnsCalculatorService returnsCalculatorService)
    {
        _returnsCalculatorService = returnsCalculatorService;
    }

    [HttpPost("returns")]
    public ActionResult<ReturnsCalculatorResponseDto> CalculateLoanEmi([FromBody] ReturnsCalculatorRequestDto request)
    {
        var result = _returnsCalculatorService.CalculateLoanEmi(request);
        return Ok(result);
    }
} 