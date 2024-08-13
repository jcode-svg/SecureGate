using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.ViewModels.Request;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.AuthServiceTests
{
    public class AuthServiceRegisterTests : AuthServiceTestsBaseSetup
    {
        [Fact]
        public async Task Register_ShouldReturnError_WhenEmployeeExists()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = _testUsername,
            };

            Employee existingEmployee = new Employee(_testEmployeeId, _testUsername);
            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync(existingEmployee);

            // Act
            var result = await AuthService.Register(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(EmployeeExistsAlready, result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenEmployeeIsRegistered()
        {
            // Arrange
            var request = new RegisterRequest
            {
                Username = _testUsername,
                // other properties
            };

            EmployeeRepositoryMock.Setup(x => x.GetEmployeeAsync(request.Username))
                .ReturnsAsync((Employee)null);

            // Act
            var result = await AuthService.Register(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Registration successful", result.ResponseObject);
            EmployeeRepositoryMock.Verify(x => x.AddNewEmployeeAsync(It.IsAny<Employee>()), Times.Once);
            EmployeeRepositoryMock.Verify(x => x.AddEmployeeBiodataAsync(It.IsAny<BioData>()), Times.Once);
            EmployeeRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
