using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class VerifyAccessPayloadValidator : AbstractValidator<VerifyAccessPayload>
    {
        public VerifyAccessPayloadValidator()
        {
            RuleFor(x => x.DoorId)
                .NotEmpty().WithMessage("Door Id is required.");
        }
    }
}
