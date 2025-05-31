using CredWiseCustomer.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CredWiseCustomer.Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithMessage("First name cannot exceed 50 characters");

            RuleFor(x => x.LastName)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.LastName))
                .WithMessage("Last name cannot exceed 50 characters");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^[6-9]\d{9}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone number must start with 6-9 and contain exactly 10 digits")
                .Length(10).When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Phone number must be 10 digits");

            RuleFor(x => x.Password)
                .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password must contain at least one special character")
                .MaximumLength(256).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage("Password cannot exceed 256 characters");
        }
    }
}
