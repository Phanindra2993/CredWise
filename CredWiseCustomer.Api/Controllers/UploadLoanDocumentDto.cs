namespace CredWiseCustomer.Api.Controllers
{
    public class UploadLoanDocumentDto
    {
        public int LoanApplicationId { get; set; }
        public string DocumentName { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
    }
} 