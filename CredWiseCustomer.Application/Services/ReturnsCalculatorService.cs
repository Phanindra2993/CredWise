using CredWiseCustomer.Application.DTOs;
using CredWiseCustomer.Application.Interfaces;
using System;

public class ReturnsCalculatorService : IReturnsCalculatorService
{
    public ReturnsCalculatorResponseDto CalculateLoanEmi(ReturnsCalculatorRequestDto request)
    {
        var P = request.Principal;
        var r = (decimal)(request.AnnualInterestRate / 12.0 / 100.0);
        var n = request.LoanTermMonths;

        if (r == 0)
        {
            return new ReturnsCalculatorResponseDto
            {
                EMI = Math.Round(P / n, 2)
            };
        }

        var emi = P * r * (decimal)Math.Pow(1 + (double)r, n) /
                  (decimal)(Math.Pow(1 + (double)r, n) - 1);

        return new ReturnsCalculatorResponseDto
        {
            EMI = Math.Round(emi, 2)
        };
    }
} 