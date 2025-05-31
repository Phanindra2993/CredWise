namespace CredWiseCustomer.Application.DTOs
{
    public class ApplyLoanDto
    {
        public int UserId { get; set; }
        public int LoanProductId { get; set; }
        public decimal RequestedAmount { get; set; }
        public int RequestedTenure { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Aadhaar { get; set; }
        public string Address { get; set; }
        public decimal Income { get; set; }
        public string EmploymentType { get; set; }
        public string CreatedBy { get; set; } = "Customer";
        public Dictionary<string, object> AdditionalDetails { get; set; } = new();
    }
} 