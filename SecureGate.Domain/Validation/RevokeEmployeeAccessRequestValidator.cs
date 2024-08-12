using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class RevokeEmployeeAccessRequestValidator : AbstractValidator<RevokeEmployeeAccessRequest>
    {
        public RevokeEmployeeAccessRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .EmailAddress().WithMessage("Username must be a valid email address.")
                .MaximumLength(50).WithMessage("Username must not be greater than 50 characters.");

            RuleFor(x => x.DoorId)
                .NotEmpty().WithMessage("Door Id is required.");
        }
    }
}
