using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Service.Tests.AccessControlServiceTests
{
    public class GrantEmployeeAccessTests
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IOfficeManagementRepository> _officeManagementRepository;
        private readonly Mock<IAccessRuleRepository> _accessRuleRepository;
        private readonly AccessService _accessService;

        public AccessServiceTests()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _officeManagementRepository = new Mock<IOfficeManagementRepository>();
            _accessRuleRepository = new Mock<IAccessRuleRepository>();
            _accessService = new AccessService(
                _employeeRepository.Object,
                _officeManagementRepository.Object,
                _accessRuleRepository.Object
            );
        }

        [Fact]
        public async Task GrantEmployeeAccess_EmployeeNotFound_ReturnsError()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest { Username = "user", DoorId = "valid-door-id" };
            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync((Employee)null);

            // Act
            var result = await _accessService.GrantEmployeeAccess(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No profile found for the third person.", result.ErrorMessage);
        }

        [Fact]
        public async Task GrantEmployeeAccess_InvalidDoorId_ReturnsError()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest { Username = "user", DoorId = "invalid-door-id" };
            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync(new Employee());
            _officeManagementRepository.Setup(repo => repo.GetDoorByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Door)null);

            // Act
            var result = await _accessService.GrantEmployeeAccess(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Could not parse door ID.", result.ErrorMessage);
        }

        [Fact]
        public async Task GrantEmployeeAccess_GrantAccess_Success()
        {
            // Arrange
            var request = new GrantEmployeeAccessRequest { Username = "user", DoorId = "valid-door-id" };
            var employee = new Employee();
            var door = new Door();
            var accessRule = (AccessRule)null; // No existing rule

            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync(employee);
            _officeManagementRepository.Setup(repo => repo.GetDoorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(door);
            _accessRuleRepository.Setup(repo => repo.GetExistingAccessRuleAsync(employee.Id, door.Id)).ReturnsAsync(accessRule);

            // Act
            var result = await _accessService.GrantEmployeeAccess(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Access granted.", result.SuccessMessage);
            _accessRuleRepository.Verify(repo => repo.AddAccessRuleAsync(It.IsAny<AccessRule>()), Times.Once);
            _accessRuleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_EmployeeNotFound_ReturnsError()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest { Username = "user", DoorId = "valid-door-id" };
            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync((Employee)null);

            // Act
            var result = await _accessService.RevokeEmployeeAccess(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No profile found for the third person.", result.ErrorMessage);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_InvalidDoorId_ReturnsError()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest { Username = "user", DoorId = "invalid-door-id" };
            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync(new Employee());
            _officeManagementRepository.Setup(repo => repo.GetDoorByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Door)null);

            // Act
            var result = await _accessService.RevokeEmployeeAccess(request);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Could not parse door ID.", result.ErrorMessage);
        }

        [Fact]
        public async Task RevokeEmployeeAccess_RevokeAccess_Success()
        {
            // Arrange
            var request = new RevokeEmployeeAccessRequest { Username = "user", DoorId = "valid-door-id" };
            var employee = new Employee();
            var door = new Door();
            var accessRule = new AccessRule { IsActive = true };

            _employeeRepository.Setup(repo => repo.GetEmployeeAsync(request.Username)).ReturnsAsync(employee);
            _officeManagementRepository.Setup(repo => repo.GetDoorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(door);
            _accessRuleRepository.Setup(repo => repo.GetExistingAccessRuleAsync(employee.Id, door.Id)).ReturnsAsync(accessRule);

            // Act
            var result = await _accessService.RevokeEmployeeAccess(request);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Access revoked.", result.SuccessMessage);
            _accessRuleRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
