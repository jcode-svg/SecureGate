using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EmployeeAggregate;
using SecureGate.Domain.Aggregates.EmployeeAggregate.DTOs;
using SecureGate.Domain.Validation;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/employeemanagement")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class EmployeeManagementController : ControllerBase
    {
        private readonly IEmployeeManagementService _employeeManagementService;
        private readonly IValidator<ApproveEmployeeRegistrationRequest> _validator;

        public EmployeeManagementController(IEmployeeManagementService employeeManagementService, IValidator<ApproveEmployeeRegistrationRequest> validator)
        {
            _employeeManagementService = employeeManagementService;
            _validator = validator;
        }

        [HttpGet("employees")]
        [ProducesResponseType(typeof(ResponseWrapper<List<EmployeeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<List<EmployeeDTO>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<List<EmployeeDTO>>>> Employees([FromQuery]PaginatedRequest request)
        {
            var result = await _employeeManagementService.GetAllEmployees(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("roles")]
        [ProducesResponseType(typeof(ResponseWrapper<List<Role>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<List<Role>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<List<Role>>>> Roles()
        {
            var result = await _employeeManagementService.GetAllRoles();

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("unapproved-employees")]
        [ProducesResponseType(typeof(ResponseWrapper<List<EmployeeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<List<EmployeeDTO>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<List<EmployeeDTO>>>> UnapprovedEmployees([FromQuery] PaginatedRequest request)
        {
            var result = await _employeeManagementService.GetUnapprovedEmployees(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("approve-employee-registration")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<string>>> ApproveEmployeeRegistration(ApproveEmployeeRegistrationRequest request)
        {
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors.Select(x => x.ErrorMessage));
            }

            var result = await _employeeManagementService.ApproveEmployeeRegistration(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
