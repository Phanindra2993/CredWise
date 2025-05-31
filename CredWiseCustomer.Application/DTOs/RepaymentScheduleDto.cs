namespace CredWiseCustomer.Application.DTOs
{
    public class RepaymentScheduleDto
    {
        public int RepaymentId { get; set; }
        public int LoanApplicationId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string? PaymentType { get; set; }
    }
} 