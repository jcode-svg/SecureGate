using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Application.Implementation
{
    public class EmployeeManagementService : IEmployeeManagementService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManagementService(IEmployeeRepository profileRepository)
        {
            _employeeRepository = profileRepository;
        }

        public async Task<ResponseWrapper<string>> ApproveEmployeeRegistration(ApproveEmployeeRegistrationRequest request)
        {
            bool roleIdParsed = Guid.TryParse(request.RoleId, out Guid parsedRoleId);

            if (!roleIdParsed)
            {
                return ResponseWrapper<string>.Error(CouldNotParseRoleId);
            }

            Employee employee = await _employeeRepository.GetEmployeeAsync(request.Username);

            if (employee == null)
            {
                return ResponseWrapper<string>.Error(NoProfileFoundThirdPerson);
            }

            if (employee.IsRegistrationApproved())
            {
                return ResponseWrapper<string>.Error(RegistrationAlreadyApproved);
            }

            if (request.Approve)
            {
                Role role = await _employeeRepository.GetRoleByIdAsync(parsedRoleId);

                if (role == null)
                {
                    return ResponseWrapper<string>.Error(RoleDoesNotExist);
                }

                employee.ApproveEmployeeRegistration(role.Id);
            }
            else
            {
                _employeeRepository.RemoveEmployee(employee);
            }

            //EF tracks the employee entity
            await _employeeRepository.SaveChangesAsync();

            return ResponseWrapper<string>.Success(EmployeeRegistrationApproved, EmployeeRegistrationApproved);
        }

        public async Task<ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>> GetAllEmployees(PaginatedRequest request)
        {
            (List<Employee> employees, bool hasNextPage) = await _employeeRepository.GetAllEmployees(request);

            if (employees == null || !employees.Any())
            {
                return ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>.Error(NoRecord);
            }

            List<EmployeeDTO> employeesResponse = employees.Select(x => x.MapToEmployeeDTO()).ToList();

            var response = new PaginatedResponse<List<EmployeeDTO>>
            {
                ResponseObject = employeesResponse,
                HasNextPage = hasNextPage
            };

            return ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>.Success(response);
        }

        public async Task<ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>> GetUnapprovedEmployees(PaginatedRequest request)
        {
            (List<Employee> unapprovedEmployees, bool hasNextPage) = await _employeeRepository.GetAllUnapprovedEmployees(request);

            if (unapprovedEmployees == null || !unapprovedEmployees.Any())
            {
                return ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>.Error(NoRecord);
            }

            List<EmployeeDTO> employeesResponse = unapprovedEmployees.Select(x => x.MapToEmployeeDTO()).ToList();

            var response = new PaginatedResponse<List<EmployeeDTO>>
            {
                ResponseObject = employeesResponse,
                HasNextPage = hasNextPage
            };

            return ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>.Success(response);
        }

        public async Task<ResponseWrapper<List<Role>>> GetAllRoles()
        {
            List<Role> roles = await _employeeRepository.GetAllRoles();

            if (roles == null || !roles.Any())
            {
                return ResponseWrapper<List<Role>>.Error(NoRecord);
            }

            return ResponseWrapper<List<Role>>.Success(roles);
        }
    }
}
