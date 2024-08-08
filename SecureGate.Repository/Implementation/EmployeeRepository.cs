using Microsoft.EntityFrameworkCore;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Infrastructure.Data;

namespace SecureGate.Repository.Implementation
{
    public class EmployeeRepository : RepositoryAbstract, IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<Employee> GetEmployeeAsync(string username)
        {
            return await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async void AddNewEmployeeAsync(Employee newEmployee)
        {
            await _applicationDbContext.Employees.AddAsync(newEmployee);
            return;
        }

        public async void AddEmployeeBiodataAsync(BioData newRecord)
        {
            await _applicationDbContext.BioData.AddAsync(newRecord);
            return;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }


    }
}
