using SecureGate.Domain.Aggregates.EmployeeAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        void AddEmployeeBiodataAsync(BioData newRecord);
        void AddNewEmployeeAsync(Employee newEmployee);
        Task<Employee> GetEmployeeAsync(string username);
        Task<bool> SaveChangesAsync();
    }
}
