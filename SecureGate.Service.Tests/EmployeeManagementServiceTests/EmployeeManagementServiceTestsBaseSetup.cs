using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.RepositoryContracts;

namespace SecureGate.Service.Tests.EmployeeManagementServiceTests
{
    public class EmployeeManagementServiceTestsBaseSetup
    {
        public EmployeeManagementService EmployeeManagementService;

        // Mocks
        public readonly Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();

        // Test Data
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly string _testUsername = "username";

        public EmployeeManagementServiceTestsBaseSetup()
        {
            // Setup mocks 
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            EmployeeManagementService = new EmployeeManagementService(EmployeeRepositoryMock.Object);
        }
    }
}
