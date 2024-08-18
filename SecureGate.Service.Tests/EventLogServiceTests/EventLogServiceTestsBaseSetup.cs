using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Service.Tests.EventLogServiceTests
{
    public class EventLogServiceTestsBaseSetup
    {
        public EventLogService EventLogService;

        // Mocks
        public readonly Mock<IOfficeManagementRepository> OfficeManagementRepositoryMock = new Mock<IOfficeManagementRepository>();
        public readonly Mock<IEventLogRepository> EventLogRepositoryMock = new Mock<IEventLogRepository>();
        public readonly Mock<IEmployeeRepository> EmployeeRepositoryMock = new Mock<IEmployeeRepository>();

        // Test Data
        protected readonly Guid _testEmployeeId = Guid.NewGuid();
        protected readonly Guid _testDoorId = Guid.NewGuid();
        protected readonly string _testUsername = "username";
        protected readonly string _testtimzoneId = "UTC";

        public EventLogServiceTestsBaseSetup()
        {
            // Setup mocks
            OfficeManagementRepositoryMock.Setup(x => x.GetDoorByIdAsync(_testDoorId))
                .ReturnsAsync(new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1));

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            // Initialize AccessControlService with mocks
            EventLogService = new EventLogService(
                EventLogRepositoryMock.Object,
                EmployeeRepositoryMock.Object,
                OfficeManagementRepositoryMock.Object
            );
        }
    }
}
