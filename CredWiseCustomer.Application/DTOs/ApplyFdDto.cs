namespace CredWiseCustomer.Application.DTOs
{
    public class ApplyFdDto
    {
        public int UserId { get; set; }
        public int FDTypeId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; } // in months
        public string CreatedBy { get; set; } = "Customer";
    }
} 