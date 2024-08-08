using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.GenericModels;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate
{
    public class AccessRule : Entity<Guid>
    {
        public AccessRule() : base(Guid.NewGuid())
        {}

        public Guid EmployeeId { get; set; }
        public Guid DoorId { get; set; }
        public Employee Employee { get; set; }
        public Door Door { get; set; }
    }
}
