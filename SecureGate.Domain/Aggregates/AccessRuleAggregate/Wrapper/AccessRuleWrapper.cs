using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate.Wrapper
{
    public class AccessRuleWrapper : IAccessRuleWrapper
    {
        public bool VerifyLevelBasedAccess(AccessLevel requiredLevel, AccessLevel userLevel)
        {
            return AccessRule.VerifyLevelBasedAccess(requiredLevel, userLevel);
        }
    }
}
