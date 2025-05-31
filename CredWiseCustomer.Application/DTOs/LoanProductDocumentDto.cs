namespace CredWiseCustomer.Application.DTOs
{
    public class LoanProductDocumentDto
    {
        public int LoanProductDocumentId { get; set; }
        public int LoanProductId { get; set; }
        public string DocumentName { get; set; }
        public bool IsActive { get; set; }
    }
} 