using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.EmployeeManagementServiceTests
{
    public class GetAllRolesTests : EmployeeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllRoles_ShouldReturnError_WhenNoRolesFound()
        {
            // Arrange
            EmployeeRepositoryMock.Setup(x => x.GetAllRoles())
                .ReturnsAsync((List<Role>)null);

            // Act
            var result = await EmployeeManagementService.GetAllRoles();

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllRoles_ShouldReturnSuccess_WhenRolesFound()
        {
            // Arrange
            var roles = new List<Role>
        {
            new Role(Guid.NewGuid(), "Admin"),
            new Role(Guid.NewGuid(), "User")
        };

            EmployeeRepositoryMock.Setup(x => x.GetAllRoles())
                .ReturnsAsync(roles);

            // Act
            var result = await EmployeeManagementService.GetAllRoles();

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(roles.Count, result.ResponseObject.Count);
        }
    }
}
