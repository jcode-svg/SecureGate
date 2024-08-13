using Moq;
using NSubstitute;
using SecureGate.Domain.Aggregates.AccessRuleAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using static SecureGate.SharedKernel.Enumerations.Enums;

namespace SecureGate.Service.Tests.AccessVerificationServiceTests
{
    public class VerifyAccessTests : AccessVerificationServiceTestsBaseSetup
    {
        [Fact]
        public async Task VerifyAccess_InvalidEmployeeId_ReturnsError()
        {
            // Arrange
            var request = new VerifyAccessRequest("invalid-guid", _testDoorId.ToString());

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.Equal(CannotCompleteRequest, result.Message);
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_InvalidDoorId_ReturnsError()
        {
            // Arrange
            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), "invalid-guid");

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.Equal(CouldNotParseDoorId, result.Message);
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_EmployeeNotFound_ReturnsError()
        {
            // Arrange
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeByIdAsync(_testEmployeeId))
                .ReturnsAsync((Employee)null);

            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), _testDoorId.ToString());

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.Equal(CannotCompleteRequest, result.Message);
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_DoorNotFound_ReturnsError()
        {
            // Arrange
            OfficeManagementRepositoryMock.Setup(x => x.GetDoorByIdAsync(_testDoorId))
                .ReturnsAsync((Door)null);

            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), _testDoorId.ToString());

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.Equal(CouldNotParseDoorId, result.Message);
            Assert.False(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_LevelBasedAccess_Granted()
        {
            // Arrange
            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), _testDoorId.ToString());

            AccessRuleWrapperMock.Setup(x => x.VerifyLevelBasedAccess(It.IsAny<AccessLevel>(), It.IsAny<AccessLevel>()))
                .Returns(true);

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.True(result.ResponseObject.AccessGranted);
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_IndividualAccess_AccessGranted()
        {
            // Arrange
            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), _testDoorId.ToString());

            var accessRule = new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true);

            AccessRuleRepositoryMock.Setup(x => x.GetActiveAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(accessRule);
            AccessRuleMock.Setup(x => x.VerifyIndividualAccess(_testEmployeeId, _testDoorId))
                .Returns(true);

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.True(result.ResponseObject.AccessGranted);
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task VerifyAccess_IndividualAccess_AccessDenied()
        {
            // Arrange
            var request = new VerifyAccessRequest(_testEmployeeId.ToString(), _testDoorId.ToString());

            var accessRule = new AccessRule(_accessRuleId, _testEmployeeId, _testDoorId, true);

            AccessRuleRepositoryMock.Setup(x => x.GetActiveAccessRuleAsync(_testEmployeeId, _testDoorId))
                .ReturnsAsync(accessRule);
            AccessRuleMock.Setup(x => x.VerifyIndividualAccess(_testEmployeeId, _testDoorId))
                .Returns(false);

            // Act
            var result = await AccessControlService.VerifyAccess(request);

            // Assert
            Assert.False(result.ResponseObject.AccessGranted);
            Assert.True(result.IsSuccessful);
        }
    }
}
