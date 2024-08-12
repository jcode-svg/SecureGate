using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureGate.Application.Contracts
{
    public interface IAccessVerificationService
    {
        Task<ResponseWrapper<VerifyAccessResponse>> VerifyAccess(VerifyAccessRequest request);
    }
}
