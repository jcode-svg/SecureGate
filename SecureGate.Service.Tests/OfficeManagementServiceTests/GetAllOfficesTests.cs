using Moq;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.OfficeManagementServiceTests
{
    public class GetAllOfficesTests : OfficeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllOffices_ShouldReturnError_WhenNoOfficesFound()
        {
            // Arrange
            OfficeManagementRepositoryMock.Setup(x => x.GetAllOfficesAsync())
                .ReturnsAsync((List<Office>)null);

            // Act
            var result = await OfficeManagementService.GetAllOffices();

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllOffices_ShouldReturnSuccess_WhenOfficesFound()
        {
            // Arrange
            var offices = new List<Office>
        {
            new Office() // Initialize with appropriate test data
        };

            OfficeManagementRepositoryMock.Setup(x => x.GetAllOfficesAsync())
                .ReturnsAsync(offices);

            // Act
            var result = await OfficeManagementService.GetAllOffices();

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Single(result.ResponseObject);
        }
    }
}
