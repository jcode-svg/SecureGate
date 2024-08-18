using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.RepositoryContracts;

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

        // Test Data
        protected readonly Guid _accessRuleId = Guid.NewGuid();
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly Guid _testDoorId = Guid.NewGuid();
        protected readonly string _testUsername = "username";

        public AccessVerificationServiceTestsBaseSetup()
        {
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
