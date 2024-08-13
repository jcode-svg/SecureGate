using Moq;
using OrderService.Infrastructure.TokenGenerator;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.RepositoryContracts;

namespace SecureGate.Service.Tests.AuthServiceTests
{
    public class AuthServiceTestsBaseSetup
    {
        public AuthService AuthService;

        // Mocks
        public readonly Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        public readonly Mock<ITokenGenerator> TokenGeneratorMock = new Mock<ITokenGenerator>();
        public readonly Mock<Employee> EmployeeMock = new Mock<Employee>();

        // Test Data
        protected readonly string _testPassword = "Password1@";
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly string _testUsername = "username";
        protected readonly string _testRole = "admin";

        public AuthServiceTestsBaseSetup()
        {
            // Setup mocks 
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            TokenGeneratorMock.Setup(x => x.GenerateToken(_testUsername, _testPassword, _testRole))
                .Returns("token");

           
            AuthService = new AuthService(
                EmployeeRepositoryMock.Object,
                TokenGeneratorMock.Object
            );
        }
    }
}
