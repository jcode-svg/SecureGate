using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.Enumerations.Enums;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using SecureGate.Domain.Aggregates.EventLogAggregate;

namespace SecureGate.Service.Tests.EventLogServiceTests
{
    public class GetAllEventsByDoorTests : EventLogServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllEventsByDoor_ShouldReturnError_WhenDoorIdCannotBeParsed()
        {
            // Arrange
            var request = new PaginatedRequest();
            var invalidDoorId = "invalid-door-id";

            // Act
            var result = await EventLogService.GetAllEventsByDoor(invalidDoorId, request, _testtimzoneId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(CouldNotParseDoorId, result.Message);
        }

        [Fact]
        public async Task GetAllEventsByDoor_ShouldReturnError_WhenDoorNotFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var doorId = Guid.NewGuid().ToString();

            OfficeManagementRepositoryMock.Setup(x => x.GetDoorByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Door)null);

            // Act
            var result = await EventLogService.GetAllEventsByDoor(doorId, request, _testtimzoneId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(CouldNotParseDoorId, result.Message);
        }

        [Fact]
        public async Task GetAllEventsByDoor_ShouldReturnError_WhenNoEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var doorId = _testDoorId.ToString();

            EventLogRepositoryMock.Setup(x => x.GetAllEventsByDoor(request, _testDoorId))
                .ReturnsAsync((new List<EventLog>(), false));

            // Act
            var result = await EventLogService.GetAllEventsByDoor(doorId, request, _testtimzoneId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllEventsByDoor_ShouldReturnSuccess_WhenEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var eventLogs = new List<EventLog>
        {
            new EventLog(new Employee(_testEmployeeId, _testUsername), new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1)
            , true, "Test Event")
        };

            EventLogRepositoryMock.Setup(x => x.GetAllEventsByDoor(request, _testDoorId))
                .ReturnsAsync((eventLogs, true));

            // Act
            var result = await EventLogService.GetAllEventsByDoor(_testDoorId.ToString(), request, _testtimzoneId);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Single(result.ResponseObject.ResponseObject);
            Assert.True(result.ResponseObject.HasNextPage);
        }
    }
}
