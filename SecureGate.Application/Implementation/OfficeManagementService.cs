using OrderService.Infrastructure.TokenGenerator;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.OfficeAggregate;
using SecureGate.Domain.Aggregates.OfficeAggregate.DTOs;
using SecureGate.Domain.RepositoryContracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using static SecureGate.SharedKernel.AppConstants.ErrorMessages;

namespace SecureGate.Application.Implementation
{
    public class OfficeManagementService : IOfficeManagementService
    {
        private readonly IOfficeManagementRepository _officeManagementRepository;

        public OfficeManagementService(IOfficeManagementRepository officeManagementRepository)
        {
            _officeManagementRepository = officeManagementRepository;
        }

        public async Task<ResponseWrapper<string>> CreateNewOffice(CreateNewOfficeRequest request)
        {
            Office newOffice = Office.CreateNewOffice(request);
            List<Door> newDoors = Door.CreateNewDoors(request.CreateDoorRequest, newOffice.Id);

            await _officeManagementRepository.AddOfficeAsync(newOffice);
            await _officeManagementRepository.AddDoorsAsync(newDoors);

            await _officeManagementRepository.SaveChangesAsync();

            return ResponseWrapper<string>.Success(OfficeCreated, OfficeCreated);
        }

        public async Task<ResponseWrapper<List<OfficeDTO>>> GetAllOffices()
        {
            List<Office> offices = await _officeManagementRepository.GetAllOfficesAsync();

            if (offices == null || !offices.Any())
            {
                return ResponseWrapper<List<OfficeDTO>>.Error(NoRecord);
            }

            List<OfficeDTO> officesResponse = offices.Select(x => x.MapToOfficeDTO()).ToList();

            return ResponseWrapper<List<OfficeDTO>>.Success(officesResponse);
        }

        public async Task<ResponseWrapper<OfficeDTO>> GetOffice(string officeId)
        {
            bool isParsed = Guid.TryParse(officeId, out Guid parsedOfficeId);

            if (!isParsed)
            {
                return ResponseWrapper<OfficeDTO>.Error(InvalidOfficeId);
            }

            Office office = await _officeManagementRepository.GetOfficeByIdAsync(parsedOfficeId);

            if (office == null)
            {
                return ResponseWrapper<OfficeDTO>.Error(InvalidOfficeId);
            }

            OfficeDTO officesResponse = office.MapToOfficeDTO();

            return ResponseWrapper<OfficeDTO>.Success(officesResponse);
        }

        public async Task<ResponseWrapper<string>> AddDoor(AddDoorRequest request)
        {
            bool isParsed = Guid.TryParse(request.OfficeId, out Guid parsedOfficeId);

            if (!isParsed)
            {
                return ResponseWrapper<string>.Error(InvalidOfficeId);
            }

            Office office = await _officeManagementRepository.GetOfficeByIdAsync(parsedOfficeId);

            if (office == null)
            {
                return ResponseWrapper<string>.Error(InvalidOfficeId);
            }

            Door newDoor = Door.CreateNewDoor(request.NewDoor, parsedOfficeId);
            await _officeManagementRepository.AddDoorAsync(newDoor);
            await _officeManagementRepository.SaveChangesAsync();

            return ResponseWrapper<string>.Success(DoorAdded, DoorAdded);
        }
    }
}
