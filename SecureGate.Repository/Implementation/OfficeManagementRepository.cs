using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.Data;

namespace SecureGate.Repository.Implementation
{
    public class OfficeManagementRepository : RepositoryAbstract, IOfficeManagementRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OfficeManagementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<List<Office>> GetAllOfficesAsync()
        {
            return await _applicationDbContext.Offices.Include(x => x.Doors).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<Office> GetOfficeByIdAsync(Guid id)
        {
            return await _applicationDbContext.Offices.Include(x => x.Doors).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Office> AddOfficeAsync(Office office)
        {
            var entity = await _applicationDbContext.Offices.AddAsync(office);
            return entity.Entity;
        }

        public async Task AddDoorAsync(Door door)
        {
            await _applicationDbContext.Doors.AddAsync(door);
        }

        public async Task AddDoorsAsync(List<Door> doors)
        {
            await _applicationDbContext.Doors.AddRangeAsync(doors);
        }

        public async Task<Door> GetDoorByIdAsync(Guid id)
        {
            return await _applicationDbContext.Doors.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }
    }
}
