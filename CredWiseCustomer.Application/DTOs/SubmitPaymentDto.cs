namespace CredWiseCustomer.Application.DTOs
{
    public class SubmitPaymentDto
    {
        public int RepaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
} 