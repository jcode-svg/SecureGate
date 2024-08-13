using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate;
using SecureGate.Domain.Aggregates.EventLogAggregate.DTOs;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Application.Implementation
{
    public class EventLogService : IEventLogService
    {
        private readonly IEventLogRepository _eventLogRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOfficeManagementRepository _officeManagementRepository;

        public EventLogService(IEventLogRepository eventLogRepository, IEmployeeRepository employeeRepository, IOfficeManagementRepository officeManagementRepository)
        {
            _eventLogRepository = eventLogRepository;
            _employeeRepository = employeeRepository;
            _officeManagementRepository = officeManagementRepository;
        }

        public async Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEvents(PaginatedRequest request)
        {
            (List<EventLog> allEvents, bool hasNextPage) = await _eventLogRepository.GetAllEvents(request);

            if (allEvents == null || !allEvents.Any())
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(NoRecord);
            }

            List<EventLogDTO> eventsDTO = allEvents.Select(x => new EventLogDTO
            {
                FirstName = x.Employee?.BioData?.FirstName,
                LastName = x.Employee?.BioData?.LastName,
                OfficeName = x.Door?.Office?.Name,
                DoorName = x.Door?.Name,
                AccessGranted = x.AccessGranted,
                Reason = x.Reason,
            }).ToList();

            var response = new PaginatedResponse<List<EventLogDTO>>
            {
                ResponseObject = eventsDTO,
                HasNextPage = hasNextPage
            };

            return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Success(response);
        }

        public async Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEventsByEmployee(string username, PaginatedRequest request)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(username);

            if (employee == null)
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(NoProfileFoundThirdPerson);
            }

            (List<EventLog> allEvents, bool hasNextPage) = await _eventLogRepository.GetAllEventsByEmployee(request, employee.Id);

            if (allEvents == null || !allEvents.Any())
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(NoRecord);
            }

            List<EventLogDTO> eventsDTO = allEvents.Select(x => new EventLogDTO
            {
                FirstName = x.Employee?.BioData?.FirstName,
                LastName = x.Employee?.BioData?.LastName,
                OfficeName = x.Door?.Office?.Name,
                DoorName = x.Door?.Name,
                AccessGranted = x.AccessGranted,
                Reason = x.Reason,
            }).ToList();

            var response = new PaginatedResponse<List<EventLogDTO>>
            {
                ResponseObject = eventsDTO,
                HasNextPage = hasNextPage
            };

            return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Success(response);
        }

        public async Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEventsByDoor(string doorId, PaginatedRequest request)
        {
            bool isParsed = Guid.TryParse(doorId, out Guid parsedDoorId);

            if (!isParsed)
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(CouldNotParseDoorId);
            }

            Door door = await _officeManagementRepository.GetDoorByIdAsync(parsedDoorId);

            if (door == null)
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(CouldNotParseDoorId);
            }

            (List<EventLog> allEvents, bool hasNextPage) = await _eventLogRepository.GetAllEventsByDoor(request, door.Id);

            if (allEvents == null || !allEvents.Any())
            {
                return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error(NoRecord);
            }

            List<EventLogDTO> eventsDTO = allEvents.Select(x => new EventLogDTO
            {
                FirstName = x.Employee?.BioData?.FirstName,
                LastName = x.Employee?.BioData?.LastName,
                OfficeName = x.Door?.Office?.Name,
                DoorName = x.Door?.Name,
                AccessGranted = x.AccessGranted,
                Reason = x.Reason,
            }).ToList();

            var response = new PaginatedResponse<List<EventLogDTO>>
            {
                ResponseObject = eventsDTO,
                HasNextPage = hasNextPage
            };

            return ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Success(response);
        }
    }
}
