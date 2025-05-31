namespace CredWiseCustomer.Application.DTOs
{
    public class FdStatusDto
    {
        public int FDApplicationId { get; set; }
        public int UserId { get; set; }
        public int FDTypeId { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }
        public decimal InterestRate { get; set; }
        public string Status { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? MaturityAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? FdTypeName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
} 