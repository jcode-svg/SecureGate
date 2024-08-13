using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate.Wrapper
{
    public interface IAccessRuleWrapper
    {
        bool VerifyIndividualAccess(Guid id, Guid employeeId, Guid doorId, bool active);
        bool VerifyLevelBasedAccess(AccessLevel requiredLevel, AccessLevel userLevel);
    }
}
