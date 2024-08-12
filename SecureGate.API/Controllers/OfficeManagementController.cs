using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EventLogAggregate.DTOs;
using SecureGate.Domain.Aggregates.OfficeAggregate.DTOs;
using SecureGate.Domain.Validation;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using SecureGate.SharedKernel.Validation;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/officemanagement")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class OfficeManagementController : ControllerBase
    {
        private readonly IOfficeManagementService _officeManagementServiceService;

        public OfficeManagementController(IOfficeManagementService officeManagementServiceService)
        {
            _officeManagementServiceService = officeManagementServiceService;
        }

        [HttpPost("create-new-office")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<string>>> CreateNewOffice(CreateNewOfficeRequest request)
        {
            var validator = new CreateNewOfficeRequestValidator().Validate(request);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors.Select(x => x.ErrorMessage));
            }

            var result = await _officeManagementServiceService.CreateNewOffice(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("offices")]
        [ProducesResponseType(typeof(ResponseWrapper<List<OfficeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<List<OfficeDTO>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<List<OfficeDTO>>>> Offices()
        {
            var result = await _officeManagementServiceService.GetAllOffices();

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("office")]
        [ProducesResponseType(typeof(ResponseWrapper<OfficeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<OfficeDTO>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<OfficeDTO>>> Office([FromQuery] string officeId)
        {
            if (string.IsNullOrWhiteSpace(officeId))
            {
                return BadRequest(ResponseWrapper<PaginatedResponse<OfficeDTO>>.Error("Office Id is required."));
            }

            var result = await _officeManagementServiceService.GetOffice(officeId);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("add-door")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<string>>> AddDoor(AddDoorRequest request)
        {
            var validator = new AddDoorRequestValidator().Validate(request);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors.Select(x => x.ErrorMessage));
            }

            var result = await _officeManagementServiceService.AddDoor(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
