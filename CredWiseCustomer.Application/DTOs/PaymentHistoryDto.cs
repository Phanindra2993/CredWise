namespace CredWiseCustomer.Application.DTOs
{
    public class PaymentHistoryDto
    {
        public int TransactionId { get; set; }
        public int LoanApplicationId { get; set; }
        public int RepaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
    }
} 