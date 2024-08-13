using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.AccessControlServiceTests
{
    public class RevokeEmployeeAccessTests : AccessControlServiceTestsBaseSetup
    {
        [Fact]
        public async Task RevokeEmployeeAccess_ShouldReturnSuccess_WhenEmployeeAndDoorExist()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessRevoked, response.Message);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_ShouldReturnError_WhenEmployeeDoesNotExist()
        {
            // Arrange
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(_testUsername))
                .ReturnsAsync((Employee)null);

            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Equal(NoProfileFoundThirdPerson, response.Message);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_ShouldReturnError_WhenDoorIdIsInvalid()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = "invalid-guid"
            };

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.False(response.IsSuccessful);
            Assert.Equal(CouldNotParseDoorId, response.Message);
        }
        [Fact]
        public async Task RevokeEmployeeAccess_ShouldReturnSuccess_WhenAccessRuleIsActive()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true));

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessRevoked, response.Message);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_ShouldDeactivateAccessRule_WhenExistingAccessRuleIsActive()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            var activeAccessRule = new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true);
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(activeAccessRule);

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessRevoked, response.Message);
            AccessRuleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_ShouldReturnSuccess_WhenAccessRuleDoesNotExist()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest
            {
                Username = _testUsername,
                DoorId = _testDoorId.ToString()
            };
            AccessRuleRepositoryMock.Setup(x => x.GetExistingAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync((AccessRule)null);

            // Act
            var response = await AccessControlService.RevokeEmployeeAccess(request);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(AccessRevoked, response.Message);
        }
    }
}
