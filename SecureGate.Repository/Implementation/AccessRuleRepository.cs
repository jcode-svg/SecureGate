using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.Data;

namespace SecureGate.Repository.Implementation
{
    public class AccessRuleRepository : RepositoryAbstract, IAccessRuleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AccessRuleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<AccessRule> GetExistingAccessRuleAsync(Guid employeeId, Guid doorId)
        {
            return await _applicationDbContext.AccessRule.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.DoorId == doorId);
        }

        public async Task<AccessRule> GetActiveAccessRuleAsync(Guid employeeId, Guid doorId)
        {
            return await _applicationDbContext.AccessRule.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.DoorId == doorId && x.Active);
        }

        public async Task<AccessRule> AddAccessRuleAsync(AccessRule newAccessRule)
        {
            var entity = await _applicationDbContext.AccessRule.AddAsync(newAccessRule);
            return entity.Entity;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }
    }
}
