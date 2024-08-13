using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.OfficeManagementServiceTests
{
    public class CreateNewOfficeTests : OfficeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task CreateNewOffice_ShouldReturnSuccess_WhenOfficeCreated()
        {
            // Arrange
            var request = new CreateNewOfficeRequest
            {
                Name = "Test",
                CreateDoorRequest = new List<CreateDoorRequest>
                {

                }
            };

            // Act
            var result = await OfficeManagementService.CreateNewOffice(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(OfficeCreated, result.Message);
        }
    }
}
