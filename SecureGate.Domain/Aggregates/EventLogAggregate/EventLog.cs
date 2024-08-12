using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.EventLogAggregate
{
    public class EventLog : Entity<Guid>
    {
        public EventLog() : base(Guid.NewGuid())
        { }

        public Guid EmployeeId { get; set; }
        public Guid DoorId { get; set; }
        public bool AccessGranted { get; set; } = false;
        public string Reason { get; set; } = string.Empty;
        public Employee Employee { get; set; }
        public Door Door { get; set; }

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
