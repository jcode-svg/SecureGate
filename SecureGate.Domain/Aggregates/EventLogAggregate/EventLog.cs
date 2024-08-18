using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.EventLogAggregate
{
    public class EventLog : Entity<Guid>
    {
        public EventLog() : base(Guid.NewGuid())
        { }

        public EventLog(Employee employee, Door door, bool accessGranted, string reason) : base(Guid.NewGuid())
        {
            Employee = employee;
            Door = door;
            AccessGranted = accessGranted;
            Reason = reason;
        }

        public Guid EmployeeId { get; private set; }
        public Guid DoorId { get; private set; }
        public bool AccessGranted { get; private set; } = false;
        public string Reason { get; private set; } = string.Empty;
        public Employee Employee { get; private set; }
        public Door Door { get; private set; }

        public static EventLog CreateEventLog(Guid employeeId, Guid doorId)
        {
            return new EventLog
            {
                EmployeeId = employeeId,
                DoorId = doorId,
            };
        }

        public void UpdateAccessStatus(bool accessGranted, string reason = "")
        {
            AccessGranted = accessGranted;
            Reason = reason;
        }
    }
}
