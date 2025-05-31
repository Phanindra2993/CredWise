using System;

namespace CredWiseCustomer.Application.DTOs
{
    public class FdPaymentScheduleDto
    {
        public int Id { get; set; }
        public int FdId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? PaidDate { get; set; }
        public string? TransactionId { get; set; }
    }
} 