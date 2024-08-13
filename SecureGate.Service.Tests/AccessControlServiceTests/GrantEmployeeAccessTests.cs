using Moq;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.AccessControlServiceTests
{
    public class GrantEmployeeAccessTests : AccessControlServiceTestsBaseSetup
    {
        [Fact]
        public async Task GrantEmployeeAccess_ShouldReturnSuccess_WhenEmployeeAndDoorExist()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessGranted, response.Message);
        }

        [Fact]
        public async Task GrantEmployeeAccess_ShouldReturnError_WhenEmployeeDoesNotExist()
        {
            // Arrange
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync((Employee)null);

            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Equal(NoProfileFoundThirdPerson, response.Message);
        }

        [Fact]
        public async Task GrantEmployeeAccess_ShouldReturnError_WhenDoorIdIsInvalid()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = "invalid-guid"
            };

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Equal(CouldNotParseDoorId, response.Message);
        }

        [Fact]
        public async Task GrantEmployeeAccess_ShouldReturnSuccess_WhenAccessRuleIsActive()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true));

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessGranted, response.Message);
        }

        [Fact]
        public async Task GrantEmployeeAccess_ShouldActivateAccessRule_WhenExistingAccessRuleIsInactive()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            var inactiveAccessRule = new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, false);
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(inactiveAccessRule);

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessGranted, response.Message);
            AccessRuleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GrantEmployeeAccess_ShouldCreateNewAccessRule_WhenNotExist()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync((AccessRule)null);

            // Act
            var response = await AccessControlService.GrantEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessGranted, response.Message);
            AccessRuleRepositoryMock.Verify(x => x.AddAccessRuleAsync(It.IsAny<AccessRule>()), Times.Once);
            AccessRuleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
