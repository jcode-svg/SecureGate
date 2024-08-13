using Moq;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.OfficeManagementServiceTests
{
    public class GetOfficeTests : OfficeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetOffice_ShouldReturnError_WhenOfficeIdCannotBeParsed()
        {
            // Arrange
            var invalidOfficeId = "invalid-office-id";

            // Act
            var result = await OfficeManagementService.GetOffice(invalidOfficeId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(InvalidOfficeId, result.Message);
        }

        [Fact]
        public async Task GetOffice_ShouldReturnError_WhenOfficeNotFound()
        {
            // Arrange
            var officeId = Guid.NewGuid().ToString();

            OfficeManagementRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Office)null);

            // Act
            var result = await OfficeManagementService.GetOffice(officeId);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(InvalidOfficeId, result.Message);
        }

        [Fact]
        public async Task GetOffice_ShouldReturnSuccess_WhenOfficeFound()
        {
            // Arrange
            var officeId = Guid.NewGuid().ToString();
            var office = new Office();

            OfficeManagementRepositoryMock.Setup(x => x.GetOfficeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(office);

            // Act
            var result = await OfficeManagementService.GetOffice(officeId);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.NotNull(result.ResponseObject);
        }
    }
}
