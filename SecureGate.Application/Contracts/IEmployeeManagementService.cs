using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Application.Contracts
{
    public interface IEmployeeManagementService
    {
        Task<ResponseWrapper<string>> ApproveEmployeeRegistration(ApproveEmployeeRegistrationRequest request);
        Task<ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>> GetAllEmployees(PaginatedRequest request);
        Task<ResponseWrapper<List<Role>>> GetAllRoles();
        Task<ResponseWrapper<PaginatedResponse<List<EmployeeDTO>>>> GetUnapprovedEmployees(PaginatedRequest request);
    }
}
