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

        public Guid EmployeeId { get;  set; }
        public Guid DoorId { get;  set; }
        public bool Active { get; private set; }
        public Employee Employee { get; set; }
        public Door Door { get; set; }

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

        public static bool VerifyLevelBasedAccess(AccessLevel employeeAccessLevel, AccessLevel doorAccessLevel)
        {
            return (int)employeeAccessLevel >= (int)doorAccessLevel;
        }

        public bool VerifyIndividualAccess(Guid employeeId, Guid doorId)
        {
            return EmployeeId == employeeId &&  DoorId == doorId;
        }
    }
}
