using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class AddDoorRequestValidator : AbstractValidator<AddDoorRequest>
    {
        public AddDoorRequestValidator()
        {
            RuleFor(x => x.OfficeId)
                .NotEmpty().WithMessage("Office Id is required.");

            RuleFor(x => x.NewDoor)
                .NotEmpty().WithMessage("New Door is required.");
        }
    }
}
