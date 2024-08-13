using Moq;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.OfficeManagementServiceTests
{
    public class AddDoorTests : OfficeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task AddDoor_ShouldReturnError_WhenOfficeIdCannotBeParsed()
        {
            // Arrange
            var invalidOfficeId = "invalid-office-id";
            var request = new AddDoorRequest { OfficeId = invalidOfficeId };

            // Act
            var result = await OfficeManagementService.AddDoor(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(InvalidOfficeId, result.Message);
        }

        [Fact]
        public async Task AddDoor_ShouldReturnError_WhenOfficeNotFound()
        {
            // Arrange
            var officeId = Guid.NewGuid().ToString();
            var request = new AddDoorRequest { OfficeId = officeId };

            OfficeManagementRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Office)null);

            // Act
            var result = await OfficeManagementService.AddDoor(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(InvalidOfficeId, result.Message);
        }

        [Fact]
        public async Task AddDoor_ShouldReturnSuccess_WhenDoorAdded()
        {
            // Arrange
            var officeId = Guid.NewGuid().ToString();
            var request = new AddDoorRequest
            {
                OfficeId = officeId,
                NewDoor = new CreateDoorRequest() // Initialize with appropriate test data
            };

            OfficeManagementRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Office()); // Return a valid Office

            // Act
            var result = await OfficeManagementService.AddDoor(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(DoorAdded, result.Message);
        }
    }
}
