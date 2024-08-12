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
            bool isAuthorized = false;
            bool employeeIdParsed = Guid.TryParse(request.EmployeeId, out Guid parsedEmployeeId);
            bool doorIdParsed = Guid.TryParse(request.DoorId, out Guid parsedDoorId);
            EventLog eventLog = EventLog.CreateEventLog(parsedEmployeeId, parsedDoorId);

            if (!employeeIdParsed)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CannotCompleteRequest);
            }

            if (!doorIdParsed)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CouldNotParseDoorId);
            }

            Employee employee = await _employeeRepository.GetEmployeeByIdAsync(parsedEmployeeId);

            if (employee == null)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CannotCompleteRequest);
            }

            Door door = await _officeManagementRepository.GetDoorByIdAsync(parsedDoorId);

            if (door == null)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(CouldNotParseDoorId);
            }

            switch (door.AccessType)
            {
                case AccessType.LevelBasedAccess:
                    isAuthorized = AccessRule.VerifyLevelBasedAccess(employee.Role.AccessLevel, door.AccessLevel);
                    break;

                case AccessType.IndividualAccess:
                    AccessRule accessRule = await _accessRuleRepository.GetActiveAccessRuleAsync(employee.Id, door.Id);

                    if (accessRule == null)
                    {
                        eventLog.UpdateAccessStatus(isAuthorized, NoAccess);
                        await _eventLogRepository.AddEventLogAsync(eventLog);
                        await _eventLogRepository.SaveChangesAsync();
                        return ResponseWrapper<VerifyAccessResponse>.Error(AccessDenied);
                    }

                    isAuthorized = accessRule.VerifyIndividualAccess(employee.Id, door.Id);
                    break;

                default:
                    break;
            }

            if (!isAuthorized)
            {
                eventLog.UpdateAccessStatus(isAuthorized, NoAccess);
                await _eventLogRepository.AddEventLogAsync(eventLog);
                await _eventLogRepository.SaveChangesAsync();
                return ResponseWrapper<VerifyAccessResponse>.Error(AccessDenied);
            }

            eventLog.UpdateAccessStatus(isAuthorized);
            await _eventLogRepository.AddEventLogAsync(eventLog);
            await _eventLogRepository.SaveChangesAsync();

            var response = new VerifyAccessResponse
            {
                AccessGranted = isAuthorized,
            };

            return ResponseWrapper<VerifyAccessResponse>.Success(response, AccessGranted);
        }
    }
}
