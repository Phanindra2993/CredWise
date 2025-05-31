namespace CredWiseCustomer.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }

    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string message) : base(message) { }
    }

    public class PaymentException : Exception
    {
        public PaymentException(string message) : base(message) { }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }

    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message) { }
    }
} 