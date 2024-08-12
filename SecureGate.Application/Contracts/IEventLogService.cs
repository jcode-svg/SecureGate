using SecureGate.Domain.Aggregates.EventLogAggregate.DTOs;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Application.Contracts
{
    public interface IEventLogService
    {
        Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEvents(PaginatedRequest request);
        Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEventsByDoor(string doorId, PaginatedRequest request);
        Task<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>> GetAllEventsByEmployee(string username, PaginatedRequest request);
    }
}
