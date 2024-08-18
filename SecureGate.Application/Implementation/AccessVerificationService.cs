using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;
using static SecureGate.SharedKernel.Enumerations.Enums;
using AccessRule = SecureGate.Domain.Aggregates.AccessRuleAggregate.AccessRule;

namespace SecureGate.Application.Implementation
{
    public class AccessVerificationService : IAccessVerificationService
    {
        private readonly IOfficeManagementRepository _officeManagementRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccessRuleRepository _accessRuleRepository;
        private readonly IEventLogRepository _eventLogRepository;

        public AccessVerificationService(IOfficeManagementRepository officeManagementRepository, IEmployeeRepository employeeRepository, IAccessRuleRepository accessRuleRepository, IEventLogRepository eventLogRepository)
        {
            _officeManagementRepository = officeManagementRepository;
            _employeeRepository = employeeRepository;
            _accessRuleRepository = accessRuleRepository;
            _eventLogRepository = eventLogRepository;
        }

        public async Task<ResponseWrapper<VerifyAccessResponse>> VerifyAccess(VerifyAccessRequest request)
        {
            if (!Guid.TryParse(request.EmployeeId, out Guid parsedEmployeeId))
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CannotCompleteRequest);
            }

            if (!Guid.TryParse(request.DoorId, out Guid parsedDoorId))
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CouldNotParseDoorId);
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(parsedEmployeeId);
            if (employee == null)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(EmployeeNotFound);
            }

            var door = await _officeManagementRepository.GetDoorByIdAsync(parsedDoorId);
            if (door == null)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CouldNotParseDoorId);
            }

            bool isAuthorized = door.AccessType switch
            {
                AccessType.LevelBasedAccess => VerifyLevelBasedAccess(employee.Role.AccessLevel, door.AccessLevel),
                AccessType.IndividualAccess => await VerifyIndividualAccessAsync(employee.Id, door.Id),
                _ => false
            };

            await LogAccessAttemptAsync(parsedEmployeeId, parsedDoorId, isAuthorized);

            if (!isAuthorized)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(AccessDenied);
            }

            var response = new VerifyAccessResponse
            {
                AccessGranted = isAuthorized,
            };

            return ResponseWrapper<VerifyAccessResponse>.Success(response, AccessGranted);
        }

        private bool VerifyLevelBasedAccess(AccessLevel employeeAccessLevel, AccessLevel doorAccessLevel)
        {
            return employeeAccessLevel >= doorAccessLevel;
        }

        private async Task<bool> VerifyIndividualAccessAsync(Guid employeeId, Guid doorId)
        {
            var accessRule = await _accessRuleRepository.GetActiveAccessRuleAsync(employeeId, doorId);
            return accessRule != null && accessRule.VerifyIndividualAccess(employeeId, doorId);
        }

        private async Task LogAccessAttemptAsync(Guid employeeId, Guid doorId, bool isAuthorized)
        {
            var eventLog = EventLog.CreateEventLog(employeeId, doorId);
            eventLog.UpdateAccessStatus(isAuthorized);
            await _eventLogRepository.AddEventLogAsync(eventLog);
            await _eventLogRepository.SaveChangesAsync();
        }
    }
}
