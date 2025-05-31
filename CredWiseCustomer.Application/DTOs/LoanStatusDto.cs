namespace CredWiseCustomer.Application.DTOs
{
    public class LoanStatusDto
    {
        public int LoanapplicationId { get; set; }
        public int UserId { get; set; }
        public int LoanProductId { get; set; }
        public decimal RequestedAmount { get; set; }
        public int RequestedTenure { get; set; }
        public string Status { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string? DecisionReason { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
} 