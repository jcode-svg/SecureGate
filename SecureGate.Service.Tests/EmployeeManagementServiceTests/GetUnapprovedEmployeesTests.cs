using Moq;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.EmployeeManagementServiceTests
{
    public class EmployeeManagementServiceGetUnapprovedEmployeesTests : EmployeeManagementServiceTestsBaseSetup
    {
        [Fact]
        public async Task GetUnapprovedEmployees_ShouldReturnError_WhenNoUnapprovedEmployeesFound()
        {
            // Arrange
            var request = new PaginatedRequest();

            EmployeeRepositoryMock.Setup(x => x.GetAllUnapprovedEmployees(request))
                .ReturnsAsync((new List<Employee>(), false));

            // Act
            var result = await EmployeeManagementService.GetUnapprovedEmployees(request);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal(NoRecord, result.Message);
        }

        [Fact]
        public async Task GetUnapprovedEmployees_ShouldReturnSuccess_WhenUnapprovedEmployeesFound()
        {
            // Arrange
            var request = new PaginatedRequest();
            var unapprovedEmployees = new List<Employee>
        {
            new Employee(Guid.NewGuid(), "username1"),
            new Employee(Guid.NewGuid(), "username2")
        };

            EmployeeRepositoryMock.Setup(x => x.GetAllUnapprovedEmployees(request))
                .ReturnsAsync((unapprovedEmployees, true));

            // Act
            var result = await EmployeeManagementService.GetUnapprovedEmployees(request);

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(unapprovedEmployees.Count, result.ResponseObject.ResponseObject.Count);
            Assert.True(result.ResponseObject.HasNextPage);
        }
    }
}
