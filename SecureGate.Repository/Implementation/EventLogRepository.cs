using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.Data;
using SecureGate.SharedKernel.HelperMethods;
using SecureGate.SharedKernel.Models;

namespace SecureGate.Repository.Implementation
{
    public class EventLogRepository : RepositoryAbstract, IEventLogRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EventLogRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<(List<EventLog> events, bool hasNextPage)> GetAllEvents(PaginatedRequest paginatedRequest)
        {
            var query = _applicationDbContext.EventLogs.Include(x => x.Employee.BioData).Include(x => x.Door.Office).OrderByDescending(record => record.CreatedAt);
            var (items, hasNextPage) = await query.ApplyTo(paginatedRequest);
            return (items.ToList(), hasNextPage);
        }

        public async Task<(List<EventLog> events, bool hasNextPage)> GetAllEventsByEmployee(PaginatedRequest paginatedRequest, Guid employeeId)
        {
            var query = _applicationDbContext.EventLogs
                .Include(x => x.Employee.BioData)
                .Include(x => x.Door.Office)
                .Where(x => x.EmployeeId == employeeId)
                .OrderByDescending(record => record.CreatedAt);
            var (items, hasNextPage) = await query.ApplyTo(paginatedRequest);
            return (items.ToList(), hasNextPage);
        }

        public async Task<(List<EventLog> events, bool hasNextPage)> GetAllEventsByDoor(PaginatedRequest paginatedRequest, Guid doorId)
        {
            var query = _applicationDbContext.EventLogs
                .Include(x => x.Employee.BioData)
                .Include(x => x.Door.Office)
                .Where(x => x.DoorId == doorId)
                .OrderByDescending(record => record.CreatedAt);
            var (items, hasNextPage) = await query.ApplyTo(paginatedRequest);
            return (items.ToList(), hasNextPage);
        }

        public async Task<EventLog> AddEventLogAsync(EventLog eventLog)
        {
            var record = await _applicationDbContext.EventLogs.AddAsync(eventLog);
            return record.Entity;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }
    }
}
