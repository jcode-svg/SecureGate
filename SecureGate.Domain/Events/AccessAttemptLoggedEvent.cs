using MediatR;

namespace SecureGate.Domain.Events
{
    public class AccessAttemptLoggedEvent : INotification
    {
        public Guid EmployeeId { get; }
        public Guid DoorId { get; }
        public bool IsAuthorized { get; }

        public AccessAttemptLoggedEvent(Guid employeeId, Guid doorId, bool isAuthorized)
        {
            EmployeeId = employeeId;
            DoorId = doorId;
            IsAuthorized = isAuthorized;
        }
    }
}
