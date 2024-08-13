using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.AccessRuleAggregate.Wrapper;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Service.Tests.AccessVerificationServiceTests
{
    public class AccessVerificationServiceTestsBaseSetup
    {
        public AccessVerificationService AccessVerificationService;

        // Mocks
        public readonly Mock<IOfficeManagementRepository> OfficeManagementRepositoryMock = new Mock<IOfficeManagementRepository>();
        public readonly Mock<IAccessRuleRepository> AccessRuleRepositoryMock = new Mock<IAccessRuleRepository>();
        public readonly Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();
        public readonly Mock<IEventLogRepository> EventLogRepositoryMock = new Mock<IEventLogRepository>();
        public readonly Mock<IAccessRuleWrapper> AccessRuleWrapperMock = new Mock<IAccessRuleWrapper>();
        public readonly Mock<AccessRule> AccessRuleMock = new Mock<AccessRule>();

        // Test Data
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly Guid _testDoorId = Guid.NewGuid();
        protected readonly Guid _accessRuleId = Guid.NewGuid();
        protected readonly string _testUsername = "username";

        public AccessVerificationServiceTestsBaseSetup()
        {
            // Setup mocks
            OfficeManagementRepositoryMock.Setup(x => x.GetDoorByIdAsync(_testDoorId))
                .ReturnsAsync(new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1));

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true));

            // Initialize AccessVerificationService with mocks
            AccessVerificationService = new AccessVerificationService(
                OfficeManagementRepositoryMock.Object,
                EmployeeRepositoryMock.Object,
                AccessRuleRepositoryMock.Object,
                EventLogRepositoryMock.Object
            );
        }
    }
}
