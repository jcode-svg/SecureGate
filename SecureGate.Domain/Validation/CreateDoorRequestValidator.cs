using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class CreateDoorRequestValidator : AbstractValidator<CreateDoorRequest>
    {
        public CreateDoorRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(20).WithMessage("Name must not be greater than 20 characters.");

            RuleFor(x => x.AccessType)
                .NotEmpty().WithMessage("Access Type is required.")
                .IsInEnum().WithMessage("Access Type must be either 1 (LevelBasedAccess) or 2 (IndividualAccess).");

            RuleFor(x => x.AccessLevel)
                .NotEmpty().WithMessage("Access Level is required.")
                .IsInEnum().WithMessage("Access Level must be 1 (Level1), 2 (Level2), or 3 (Level3).");
        }
    }
}
