using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Domain.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        Task AddEmployeeBiodataAsync(BioData newRecord);
        Task AddNewEmployeeAsync(Employee newEmployee);
        Task<(List<Employee> employees, bool hasNextPage)> GetAllEmployees(PaginatedRequest paginatedRequest);
        Task<List<Role>> GetAllRoles();
        Task<(List<Employee> employees, bool hasNextPage)> GetAllUnapprovedEmployees(PaginatedRequest paginatedRequest);
        Task<Employee> GetEmployeeAsync(string username);
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId);
        Task<Role> GetRoleByIdAsync(Guid roleId);
        void RemoveEmployee(Employee employee);
        Task<bool> SaveChangesAsync();
    }
}
