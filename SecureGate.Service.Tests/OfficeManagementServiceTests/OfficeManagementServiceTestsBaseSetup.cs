using Moq;
using SecureGate.Application.Implementation;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using static SecureGate.SharedKernel.Enumerations.Enums;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Service.Tests.OfficeManagementServiceTests
{
    public class OfficeManagementServiceTestsBaseSetup
    {
        public OfficeManagementService OfficeManagementService;

        // Mocks
        public readonly Mock<IOfficeManagementRepository> OfficeManagementRepositoryMock = new Mock<IOfficeManagementRepository>();

        // Test Data
        protected readonly Guid _testDoorId = Guid.NewGuid();

        public OfficeManagementServiceTestsBaseSetup()
        {
            OfficeManagementService = new OfficeManagementService(
                OfficeManagementRepositoryMock.Object
            );
        }
    }
}
