using MediatR;
using SecureGate.Domain.Events;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Response;
using static SecureGate.SharedKernel.Enumerations.Enums;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Application.Commands
{
    public class VerifyAccessCommandHandler : IRequestHandler<VerifyAccessCommand, ResponseWrapper<VerifyAccessResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOfficeManagementRepository _officeManagementRepository;
        private readonly IAccessRuleRepository _accessRuleRepository;
        private readonly IMediator _mediator;

        public VerifyAccessCommandHandler(
            IEmployeeRepository employeeRepository,
            IOfficeManagementRepository officeManagementRepository,
            IAccessRuleRepository accessRuleRepository,
            IMediator mediator)
        {
            _employeeRepository = employeeRepository;
            _officeManagementRepository = officeManagementRepository;
            _accessRuleRepository = accessRuleRepository;
            _mediator = mediator;
        }

        public async Task<ResponseWrapper<VerifyAccessResponse>> Handle(VerifyAccessCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                return ResponseWrapper<VerifyAccessResponse>.Error(EmployeeNotFound);
            }

            var door = await _officeManagementRepository.GetDoorByIdAsync(request.DoorId);
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

            await _mediator.Publish(new AccessAttemptLoggedEvent(request.EmployeeId, request.DoorId, isAuthorized), cancellationToken);

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
    }
}
