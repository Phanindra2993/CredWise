public class ReturnsCalculatorRequestDto
{
    public decimal Principal { get; set; }
    public double AnnualInterestRate { get; set; } // in percent
    public int LoanTermMonths { get; set; }
} 