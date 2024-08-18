using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.Enumerations.Enums;
using Moq;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.EventLogServiceTests
{
    public class GetAllEventsTests : EventLogServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllEvents_ShouldReturnError_WhenNoEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();

            EventLogRepositoryMock.Setup(x => x.GetAllEvents(request))
                .ReturnsAsync((new List<EventLog>(), false));

            // Act
            var result = await EventLogService.GetAllEvents(request, _testtimzoneId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllEvents_ShouldReturnSuccess_WhenEventsFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var eventLogs = new List<EventLog>
        {
            new EventLog(new Employee(_testEmployeeId, _testUsername), new Door(_testDoorId, AccessType.LevelBasedAccess, AccessLevel.Level1)
            , true, "Test Event")
        };

            EventLogRepositoryMock.Setup(x => x.GetAllEvents(request))
                .ReturnsAsync((eventLogs, true));

            // Act
            var result = await EventLogService.GetAllEvents(request, _testtimzoneId);
                
            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Single(result.ResponseObject.ResponseObject);
            Assert.True(result.ResponseObject.HasNextPage);
        }
    }
}
