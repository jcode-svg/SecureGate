using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.Enumerations.Enums;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using SecureGate.Domain.Aggregates.EventLogAggregate;

namespace SecureGate.Service.Tests.EventLogServiceTests
{
    public class GetAllEventsByEmployeeTests : EventLogServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllEventsByEmployee_ShouldReturnError_WhenEmployeeNotFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var username = "nonexistentUsername";

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(username))
                .ReturnsAsync((Employee)null);

            // Act
            var result = await EventLogService.GetAllEventsByEmployee(username, request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoProfileFoundThirdPerson, result.Message);
        }

        [Fact]
        public async Task GetAllEventsByEmployee_ShouldReturnError_WhenNoEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            EventLogRepositoryMock.Setup(x => x.GetAllEventsByEmployee(request, _testEmployeeId))
                .ReturnsAsync((new List<EventLog>(), false));

            // Act
            var result = await EventLogService.GetAllEventsByEmployee(_testUsername, request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllEventsByEmployee_ShouldReturnSuccess_WhenEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var eventLogs = new List<EventLog>
        {
            new EventLog
            {
                Employee = new Employee(_testEmployeeId, _testUsername),
                Door = new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1),
                AccessGranted = true,
                Reason = "Test Event"
            }
        };

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync(new Employee(_testEmployeeId, _testUsername));

            EventLogRepositoryMock.Setup(x => x.GetAllEventsByEmployee(request, _testEmployeeId))
                .ReturnsAsync((eventLogs, true));

            // Act
            var result = await EventLogService.GetAllEventsByEmployee(_testUsername, request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Single(result.ResponseObject.ResponseObject);
            Assert.True(result.ResponseObject.HasNextPage);
        }
    }
}
