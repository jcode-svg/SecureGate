using FluentValidation;
using SecureGate.Domain.ViewModels.Request;

namespace SecureGate.Domain.Validation
{
    public class CreateNewOfficeRequestValidator : AbstractValidator<CreateNewOfficeRequest>
    {
        public CreateNewOfficeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(20).WithMessage("Name must not be greater than 20 characters.");

            RuleFor(x => x.CreateDoorRequest)
                .NotEmpty().WithMessage("Create Door Requests are required.")
                .Must(list => list != null && list.Any()).WithMessage("Create Door Requests cannot be empty.")
                .ForEach(doorRequest =>
                    doorRequest.SetValidator(new CreateDoorRequestValidator()));
        }
    }
}
