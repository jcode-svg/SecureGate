using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using System;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate.Wrapper
{
    public class AccessRuleWrapper : IAccessRuleWrapper
    {
        public bool VerifyLevelBasedAccess(AccessLevel requiredLevel, AccessLevel userLevel)
        {
            return AccessRule.VerifyLevelBasedAccess(requiredLevel, userLevel);
        }

        public bool VerifyIndividualAccess(Guid id, Guid employeeId, Guid doorId, bool active)
        {
            return new AccessRule(id, employeeId, doorId, active).VerifyIndividualAccess(employeeId, doorId);
        }
    }
}
