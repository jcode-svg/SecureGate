using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Domain.Aggregates.AccessRuleAggregate.Wrapper
{
    public interface IAccessRuleWrapper
    {
        bool VerifyLevelBasedAccess(AccessLevel requiredLevel, AccessLevel userLevel);
    }
}
