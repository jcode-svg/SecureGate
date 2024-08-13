using FluentValidation;
using SecureGate.Domain.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.Validation
{
    public class GrantEmployeeAccessRequestValidator : AbstractValidator<GrantEmployeeAccessRequest>
    {
        public GrantEmployeeAccessRequestValidator()
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
