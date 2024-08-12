using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using System.Security.AccessControl;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using AccessRule = SecureGate.Domain.Aggregates.AccessRuleAggregate.AccessRule;

namespace SecureGate.Application.Implementation
{
    public class AccessControlService : IAccessControlService
    {
        private readonly IOfficeManagementRepository _officeManagementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccessRuleRepository _accessRuleRepository;

        public AccessControlService(IOfficeManagementRepository officeManagementRepository, IEmployeeRepository employeeRepository, IAccessRuleRepository accessRuleRepository)
        {
            _officeManagementRepository = officeManagementRepository;
            _employeeRepository = employeeRepository;
            _accessRuleRepository = accessRuleRepository;
        }

        public async Task<ResponseWrapper<string>> GrantEmployeeAccess(GrantEmployeeAccessRequest request)
        {
            var response = await ProcessAccessRequest(request.Username, request.DoorId, true);
            return response;
        }

        public async Task<ResponseWrapper<string>> RevokeEmployeeAccess(RevokeEmployeeAccessRequest request)
        {
            var response = await ProcessAccessRequest(request.Username, request.DoorId, false);
            return response;
        }

        private async Task<ResponseWrapper<string>> ProcessAccessRequest(string username, string doorId, bool grantAccess)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(username);

            if (employee == null)
            {
                return ResponseWrapper<string>.Error(NoProfileFoundThirdPerson);
            }

            if (!Guid.TryParse(doorId, out Guid parsedDoorId))
            {
                return ResponseWrapper<string>.Error(CouldNotParseDoorId);
            }

            Door door = await _officeManagementRepository.GetDoorByIdAsync(parsedDoorId);

            if (door == null)
            {
                return ResponseWrapper<string>.Error(CouldNotParseDoorId);
            }

            AccessRule existingAccessRule = await _accessRuleRepository.GetExistingAccessRuleAsync(employee.Id, door.Id);

            if (existingAccessRule != null)
            {
                if (grantAccess)
                {
                    if (!existingAccessRule.IsActive())
                    {
                        existingAccessRule.Activate();
                        await _accessRuleRepository.SaveChangesAsync();
                    }
                }
                else
                {
                    if (existingAccessRule.IsActive())
                    {
                        existingAccessRule.Deactivate();
                        await _accessRuleRepository.SaveChangesAsync();
                    }
                }
            }
            else if (grantAccess)
            {
                AccessRule newAccessRule = AccessRule.CreateNewAccess(employee.Id, door.Id);
                await _accessRuleRepository.AddAccessRuleAsync(newAccessRule);
                await _accessRuleRepository.SaveChangesAsync();
            }

            string resultMessage = grantAccess ? AccessGranted : AccessRevoked;
            return ResponseWrapper<string>.Success(resultMessage, resultMessage);
        }
    }
}
