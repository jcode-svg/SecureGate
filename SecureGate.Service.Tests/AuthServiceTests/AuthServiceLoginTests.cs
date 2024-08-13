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

            var employee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);
            EmployeeMock.Setup(x => x.IsRegistrationApproved())
                .Returns(false);

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
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);
            EmployeeMock.Setup(x => x.IsPasswordValid(request.Password, out errorMessage))
                .Returns(false)
                .Callback((string password, out string error) => error = "Invalid password");

            // Act
            var result = await AuthService.Login(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Invalid password", result.Message);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenLoginIsValid()
        {
            // Arrange
            string errorMessage = string.Empty;
            var request = new LoginRequest
            {
                Username = _testUsername,
                Password = _testPassword
            };

            var employee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(employee);
            EmployeeMock.Setup(x => x.IsPasswordValid(request.Password, out errorMessage))
                .Returns(true);

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
