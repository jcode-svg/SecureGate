using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.RepositoryContracts
{
    public interface IEventLogRepository
    {
        Task<EventLog> AddEventLogAsync(EventLog eventLog);
        Task<(List<EventLog> events, bool hasNextPage)> GetAllEvents(PaginatedRequest paginatedRequest);
        Task<(List<EventLog> events, bool hasNextPage)> GetAllEventsByDoor(PaginatedRequest paginatedRequest, Guid doorId);
        Task<(List<EventLog> events, bool hasNextPage)> GetAllEventsByEmployee(PaginatedRequest paginatedRequest, Guid employeeId);
        Task<bool> SaveChangesAsync();
    }
}
