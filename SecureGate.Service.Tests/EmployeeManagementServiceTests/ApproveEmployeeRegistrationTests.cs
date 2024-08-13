using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.EmployeeManagementServiceTests
{
    public class EmployeeManagementServiceApproveEmployeeRegistrationTests : EmployeeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldReturnError_WhenRoleIdCannotBeParsed()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = "invalid-role-id",
                Username = _testUsername,
                Approve = true
            };

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(CouldNotParseRoleId, result.Message);
        }

        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldReturnError_WhenEmployeeNotFound()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = Guid.NewGuid().ToString(),
                Username = _testUsername,
                Approve = true
            };

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync((Employee)null);

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoProfileFoundThirdPerson, result.Message);
        }

        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldReturnError_WhenRegistrationAlreadyApproved()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = Guid.NewGuid().ToString(),
                Username = _testUsername,
                Approve = true
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            employee.ApproveEmployeeRegistration(Guid.NewGuid()); // Already approved
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(RegistrationAlreadyApproved, result.Message);
        }

        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldReturnError_WhenRoleDoesNotExist()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = Guid.NewGuid().ToString(),
                Username = _testUsername,
                Approve = true
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);
            EmployeeRepositoryMock.Setup(x => x.GetRoleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Role)null);

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(RoleDoesNotExist, result.Message);
        }

        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldReturnSuccess_WhenApproved()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = Guid.NewGuid().ToString(),
                Username = _testUsername,
                Approve = true
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            var role = new Role(Guid.NewGuid(), "Admin");
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);
            EmployeeRepositoryMock.Setup(x => x.GetRoleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(role);

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(EmployeeRegistrationApproved, result.Message);
            EmployeeRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ApproveEmployeeRegistration_ShouldRemoveEmployee_WhenNotApproved()
        {
            // Arrange
            var request = new ApproveEmployeeRegistrationRequest
            {
                RoleId = Guid.NewGuid().ToString(),
                Username = _testUsername,
                Approve = false
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);

            // Act
            var result = await EmployeeManagementService.ApproveEmployeeRegistration(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(EmployeeRegistrationApproved, result.Message);
            EmployeeRepositoryMock.Verify(x => x.RemoveEmployee(employee), Times.Once);
            EmployeeRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
