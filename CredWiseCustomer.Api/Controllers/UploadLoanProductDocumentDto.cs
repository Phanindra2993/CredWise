namespace CredWiseCustomer.Api.Controllers
{
    public class UploadLoanProductDocumentDto
    {
        public int LoanProductId { get; set; }
        public int? LoanApplicationId { get; set; }
        public string DocumentName { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
} 