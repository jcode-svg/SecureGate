using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Validation;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using SecureGate.SharedKernel.Validation;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/accessverification")]
    [ApiController]
    [Authorize]
    public class AccessVerificationController : ControllerBase
    {
        private readonly IAccessVerificationService _accessVerificationService;

        public AccessVerificationController(IAccessVerificationService accessVerificationService)
        {
            _accessVerificationService = accessVerificationService;
        }

        [HttpPost("verify-access")]
        [ProducesResponseType(typeof(ResponseWrapper<VerifyAccessResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<VerifyAccessResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<VerifyAccessResponse>>> VerifyAccess(VerifyAccessPayload payload)
        {
            var validator = new VerifyAccessPayloadValidator().Validate(payload);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors.Select(x => x.ErrorMessage));
            }

            string employeeId = EmployeeId();

            VerifyAccessRequest request = new VerifyAccessRequest(employeeId, payload.DoorId);

            var result = await _accessVerificationService.VerifyAccess(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        private string EmployeeId() => HttpContext?.User?.Claims.FirstOrDefault(claim => claim.Type == SecureGateClaims.ProfileId)?.Value;
    }
}
