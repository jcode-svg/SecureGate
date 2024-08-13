using SecureGate.Domain.Aggregates.OfficeAggregate.DTOs;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Application.Contracts
{
    public interface IOfficeManagementService
    {
        Task<ResponseWrapper<string>> AddDoor(AddDoorRequest request);
        Task<ResponseWrapper<string>> CreateNewOffice(CreateNewOfficeRequest request);
        Task<ResponseWrapper<List<OfficeDTO>>> GetAllOffices();
        Task<ResponseWrapper<OfficeDTO>> GetOffice(string officeId);
    }
}
