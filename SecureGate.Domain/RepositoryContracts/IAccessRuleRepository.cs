using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.RepositoryContracts
{
    public interface IAccessRuleRepository
    {
        Task<AccessRule> AddAccessRuleAsync(AccessRule newAccessRule);
        Task<AccessRule> GetActiveAccessRuleAsync(Guid employeeId, Guid doorId);
        Task<AccessRule> GetExistingAccessRuleAsync(Guid employeeId, Guid doorId);
        Task<bool> SaveChangesAsync();
    }
}
