using MediatR;
using SecureGate.Domain.ViewModels.Response;

namespace SecureGate.Application.Commands
{
    public class VerifyAccessCommand : IRequest<ResponseWrapper<VerifyAccessResponse>>
    {
        public Guid EmployeeId { get; }
        public Guid DoorId { get; }

        public VerifyAccessCommand(Guid employeeId, Guid doorId)
        {
            EmployeeId = employeeId;
            DoorId = doorId;
        }
    }
}
