using OrderService.Infrastructure.TokenGenerator;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Application.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(IEmployeeRepository profileRepository, ITokenGenerator tokenGenerator)
        {
            _employeeRepository = profileRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResponseWrapper<string>> Register(RegisterRequest request)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(request.Username);

            if (employee != null)
            {
                return ResponseWrapper<string>.Error(EmployeeExistsAlready);
            }

            Employee newEmployee = Employee.CreateNewEmployee(request);
            BioData bioData = BioData.CreateEmployeeBiodata(newEmployee.Id, request);
            newEmployee.UpdateBiodataId(bioData.Id);

            await _employeeRepository.AddNewEmployeeAsync(newEmployee);
            await _employeeRepository.AddEmployeeBiodataAsync(bioData);

            await _employeeRepository.SaveChangesAsync();

            return ResponseWrapper<string>.Success("Registration successful");
        }

        public async Task<ResponseWrapper<LoginResponse>> Login(LoginRequest request)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(request.Username);

            if (employee == null)
            {
                return ResponseWrapper<LoginResponse>.Error(NoProfileFound);
            }

            if (!employee.IsRegistrationApproved())
            {
                return ResponseWrapper<LoginResponse>.Error(RegistrationNotApproved);
            }

            if (!employee.IsPasswordValid(request.Password, out string errorMessage))
            {
                return ResponseWrapper<LoginResponse>.Error(errorMessage);
            }

            var response = new LoginResponse
            {
                Token = _tokenGenerator.GenerateToken(request.Username, employee.Id.ToString(), employee.Role.Name)
            };

            return ResponseWrapper<LoginResponse>.Success(response);
        }
    }
}
