using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Contracts;
using SecureGate.Domain.Aggregates.EventLogAggregate.DTOs;
using SecureGate.Domain.ViewModels.Response;
using SecureGate.SharedKernel.Models;
using System;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/eventlog")]
    [ApiController]
    [Authorize(Policy = "AdminPolicy")]
    public class EventLogController : ControllerBase
    {
        private readonly IEventLogService _eventLogService;

        public EventLogController(IEventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [HttpGet("events")]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>>> Events([FromQuery] PaginatedRequest request
            , [FromHeader(Name = "TimeZone")] string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId) || !TimeZoneInfo.GetSystemTimeZones().Any(tz => tz.Id == timeZoneId))
            {
                return BadRequest(new ResponseWrapper<PaginatedResponse<List<EventLogDTO>>> { Message = "Invalid or missing TimeZoneId header." });
            }

            var result = await _eventLogService.GetAllEvents(request, timeZoneId);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("events-by-employee")]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>>> EventsByEmployee([FromQuery] string username, [FromQuery] PaginatedRequest request,
            [FromHeader(Name = "TimeZone")] string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId) || !TimeZoneInfo.GetSystemTimeZones().Any(tz => tz.Id == timeZoneId))
            {
                return BadRequest(new ResponseWrapper<PaginatedResponse<List<EventLogDTO>>> { Message = "Invalid or missing TimeZoneId header." });
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error("Username is required."));
            }

            var result = await _eventLogService.GetAllEventsByEmployee(username, request, timeZoneId);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("events-by-door")]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>>> EventsByDoor([FromQuery] string doorId, [FromQuery] PaginatedRequest request
            , [FromHeader(Name = "TimeZone")] string timeZoneId)
        {
            if (string.IsNullOrEmpty(timeZoneId) || !TimeZoneInfo.GetSystemTimeZones().Any(tz => tz.Id == timeZoneId))
            {
                return BadRequest(new ResponseWrapper<PaginatedResponse<List<EventLogDTO>>> { Message = "Invalid or missing TimeZoneId header." });
            }

            if (string.IsNullOrWhiteSpace(doorId))
            {
                return BadRequest(ResponseWrapper<PaginatedResponse<List<EventLogDTO>>>.Error("Door Id is required."));
            }

            var result = await _eventLogService.GetAllEventsByDoor(doorId, request, timeZoneId);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
