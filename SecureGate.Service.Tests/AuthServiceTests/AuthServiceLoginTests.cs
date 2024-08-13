using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.AuthServiceTests
{
    public class AuthServiceLoginTests : AuthServiceTestsBaseSetup
    {
        [Fact]
        public async Task Login_ShouldReturnError_WhenEmployeeNotFound()
        {
            // Arrange
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync((Employee)null);

            // Act
            var result = await AuthService.Login(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoProfileFound, result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnError_WhenRegistrationNotApproved()
        {
            // Arrange
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            var employee = new Employee(_testEmployeeId, _testUsername, false);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);

            // Act
            var result = await AuthService.Login(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(RegistrationNotApproved, result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnError_WhenPasswordIsInvalid()
        {
            // Arrange
            string errorMessage = string.Empty;
            string passwordHash = "";
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            var employee = new Employee(_testEmployeeId, _testUsername, true, passwordHash);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);

            // Act
            var result = await AuthService.Login(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(IncorrectPassword, result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenLoginIsValid()
        {
            // Arrange
            string passwordHash = "238ad148a42c3452f95fad480b2a5adf31151f6c7f3e1777030ee19edc4c9a73e20099e2f7c15c8f3e820614bc5175a9fe5a1b3c7c9bc3e3006c581bbe3aafa8";
            string errorMessage = string.Empty;
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            var employee = new Employee(_testEmployeeId, _testUsername, true, passwordHash);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);

            TokenGeneratorMock.Setup(x => x.GenerateToken(request.Username, _testEmployeeId.ToString(), employee.Role.Name))
                .Returns("token");

            // Act
            var result = await AuthService.Login(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("token", result.ResponseObject.Token);
        }
    }
}
