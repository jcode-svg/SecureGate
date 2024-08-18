using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.GenericModels;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate
{
    public class AccessRule : Entity<Guid>
    {
        public AccessRule() : base(Guid.NewGuid())
        { }

        public AccessRule(Guid id, Guid employeeId, Guid doorId, bool active) : base(id)
        {
            EmployeeId = employeeId;
            DoorId = doorId;
            Active = active;
        }

        public Guid EmployeeId { get;  private set; }
        public Guid DoorId { get;  private set; }
        public bool Active { get; private set; }
        public Employee Employee { get; private set; }
        public Door Door { get; private set; }

        public static AccessRule CreateNewAccess(Guid employeeId, Guid doorId)
        {
            return new AccessRule
            {
                EmployeeId = employeeId,
                DoorId = doorId,
                Active = true
            };
        }

        public bool IsActive()
        {
            return Active;
        }

        public void Activate()
        {
            Active = true;
        }

        public void Deactivate()
        {
            Active = false;
        }

        public bool VerifyIndividualAccess(Guid employeeId, Guid doorId)
        {
            return EmployeeId == employeeId &&  DoorId == doorId;
        }
    }
}
