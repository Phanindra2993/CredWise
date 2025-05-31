using CredWiseCustomer.Application.DTOs;

public interface IReturnsCalculatorService
{
    ReturnsCalculatorResponseDto CalculateLoanEmi(ReturnsCalculatorRequestDto request);
} 