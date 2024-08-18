using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Commands;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Validation;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/accessverification-eventdriven")]
    [ApiController]
    [Authorize]
    public class AccessVerificationEventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccessVerificationEventController(IMediator mediator)
        {
            _mediator = mediator;
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

            var command = new VerifyAccessCommand(Guid.Parse(employeeId), Guid.Parse(payload.DoorId));

            var result = await _mediator.Send(command);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        private string EmployeeId() => HttpContext?.User?.Claims.FirstOrDefault(claim => claim.Type == SecureGateClaims.ProfileId)?.Value;
    }
}
