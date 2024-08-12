using SecureGate.Domain.Aggregates.OfficeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.RepositoryContracts
{
    public interface IOfficeManagementRepository
    {
        Task AddDoorAsync(Door door);
        Task AddDoorsAsync(List<Door> doors);
        Task<Office> AddOfficeAsync(Office office);
        Task<List<Office>> GetAllOfficesAsync();
        Task<Door> GetDoorByIdAsync(Guid id);
        Task<Office> GetOfficeByIdAsync(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
