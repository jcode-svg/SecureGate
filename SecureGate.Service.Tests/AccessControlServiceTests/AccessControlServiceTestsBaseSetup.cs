using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Service.Tests.AccessControlServiceTests
{
    public class AccessControlServiceTestsBaseSetup
    {
        public AccessControlService AccessControlService;

        // Mocks
        public readonly Mock<IOfficeManagementRepository> OfficeManagementRepositoryMock = new Mock<IOfficeManagementRepository>();
        public readonly Mock<IAccessRuleRepository> AccessRuleRepositoryMock = new Mock<IAccessRuleRepository>();
        public readonly Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();

        // Test Data
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly Guid _testDoorId = Guid.NewGuid();
        protected readonly Guid _accessRuleId = Guid.NewGuid();
        protected readonly string _testUsername = "username";

        public AccessControlServiceTestsBaseSetup()
        {
            // Setup mocks with realistic data
            OfficeManagementRepositoryMock.Setup(x => x.GetDoorByIdAsync(_testDoorId))
                .ReturnsAsync(new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1));

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true));

            // Initialize AccessControlService with mocks
            AccessControlService = new AccessControlService(
                OfficeManagementRepositoryMock.Object,
                EmployeeRepositoryMock.Object,
                AccessRuleRepositoryMock.Object
            );
        }
    }
}
