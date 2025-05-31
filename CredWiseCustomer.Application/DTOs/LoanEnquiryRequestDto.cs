public class LoanEnquiryRequestDto
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public decimal LoanAmount { get; set; }
    public required string LoanPurpose { get; set; }
} 