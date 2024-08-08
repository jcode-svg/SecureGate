
using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.SharedKernel.Validation
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .EmailAddress().WithMessage("Username must be a valid email address.")
                .MaximumLength(50).WithMessage("Username must not be greater than 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(8, 15).WithMessage("Password must be between 8 and 15 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[.,@]").WithMessage("Password must contain at least one symbol (., or @).");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Confirm Password is required.")
                .Equal(x => x.Password).WithMessage("Password and Confirm Password must be the same.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(20).WithMessage("First Name must not be greater than 20 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(20).WithMessage("Last Name must not be greater than 20 characters.");
        }
    }
}
