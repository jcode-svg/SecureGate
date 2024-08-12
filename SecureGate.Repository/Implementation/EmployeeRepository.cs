using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.Data;
using SecureGate.SharedKernel.HelperMethods;
using SecureGate.SharedKernel.Models;

namespace SecureGate.Repository.Implementation
{
    public class EmployeeRepository : RepositoryAbstract, IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await _applicationDbContext.Employees.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == employeeId);
        }

        public async Task<Employee> GetEmployeeAsync(string username)
        {
            return await _applicationDbContext.Employees.Include(x => x.Role).FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task AddNewEmployeeAsync(Employee newEmployee)
        {
            await _applicationDbContext.Employees.AddAsync(newEmployee);
            return;
        }

        public void RemoveEmployee(Employee employee)
        {
            _applicationDbContext.Employees.Remove(employee);
        }

        public async Task AddEmployeeBiodataAsync(BioData newRecord)
        {
            await _applicationDbContext.BioData.AddAsync(newRecord);
            return;
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            return await _applicationDbContext.Roles.FindAsync(roleId);
        }

        public async Task<(List<Employee> employees, bool hasNextPage)> GetAllEmployees(PaginatedRequest paginatedRequest)
        {
            var query = _applicationDbContext.Employees.Include(record => record.BioData).OrderByDescending(record => record.CreatedAt);
            var (items, hasNextPage) = await query.ApplyTo(paginatedRequest);
            return (items.ToList(), hasNextPage);
        }

        public async Task<(List<Employee> employees, bool hasNextPage)> GetAllUnapprovedEmployees(PaginatedRequest paginatedRequest)
        {
            var query = _applicationDbContext.Employees.Include(record => record.BioData).Where(record => !record.RegistrationApproved).OrderBy(record => record.CreatedAt);
            var (items, hasNextPage) = await query.ApplyTo(paginatedRequest);
            return (items.ToList(), hasNextPage);
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _applicationDbContext.Roles.OrderByDescending(record => record.CreatedAt).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }
    }
}
