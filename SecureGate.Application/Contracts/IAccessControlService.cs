using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Application.Contracts
{
    public interface IAccessControlService
    {
        Task<ResponseWrapper<string>> GrantEmployeeAccess(GrantEmployeeAccessRequest request);
        Task<ResponseWrapper<string>> RevokeEmployeeAccess(RevokeEmployeeAccessRequest request);
    }
}
