using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class ApproveEmployeeRegistrationRequestValidator : AbstractValidator<ApproveEmployeeRegistrationRequest>
    {
        public ApproveEmployeeRegistrationRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .EmailAddress().WithMessage("Username must be a valid email address.")
                .MaximumLength(50).WithMessage("Username must not be greater than 50 characters.");

            RuleFor(x => x.Approve)
                .NotEmpty().WithMessage("Approve is required.");

            RuleFor(x => x.RoleId)
                .NotEmpty().WithMessage("RoleId is required.");
        }
    }
}
