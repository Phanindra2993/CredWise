using CredWiseCustomer.Application.DTOs;
using FluentValidation;

namespace CredWiseCustomer.Application.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        // Email Validation
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .Must(email => email.EndsWith("@gmail.com") || email.EndsWith("@credwise.com"))
                .WithMessage("Only @gmail.com and @credwise.com domains are allowed")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        // Phone Number Validation
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^[6-9]\d{9}$").WithMessage("Phone number must start with 6-9 and contain exactly 10 digits")
            .Length(10).WithMessage("Phone number must be 10 digits");

        // Password Validation
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character")
            .MaximumLength(256).WithMessage("Password cannot exceed 256 characters");

        // Name Validations
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");
    }
}