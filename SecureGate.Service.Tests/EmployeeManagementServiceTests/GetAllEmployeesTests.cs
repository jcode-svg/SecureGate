using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.EmployeeManagementServiceTests
{
    public class EmployeeManagementServiceGetAllEmployeesTests : EmployeeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetAllEmployees_ShouldReturnError_WhenNoEmployeesFound()
        {
            // Arrange
            var request = new PaginatedRequest();

            EmployeeRepositoryMock.Setup(x => x.GetAllEmployees(request))
                .ReturnsAsync((new List<Employee>(), false));

            // Act
            var result = await EmployeeManagementService.GetAllEmployees(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnSuccess_WhenEmployeesFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var employees = new List<Employee>
        {
            new Employee(Guid.NewGuid(), "username1"),
            new Employee(Guid.NewGuid(), "username2")
        };

            EmployeeRepositoryMock.Setup(x => x.GetAllEmployees(request))
                .ReturnsAsync((employees, true));

            // Act
            var result = await EmployeeManagementService.GetAllEmployees(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(employees.Count, result.ResponseObject.ResponseObject.Count);
            Assert.True(result.ResponseObject.HasNextPage);
        }
    }
}
