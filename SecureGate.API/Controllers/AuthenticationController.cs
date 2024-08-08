using Microsoft.AspNetCore.Mvc;
using SecureGate.Application.Contracts;
using SecureGate.Domain.ViewModels.Request;
using SecureGate.Domain.ViewModels.Response;
using System.Net.Mime;

namespace SecureGate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<string>>> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseWrapper<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseWrapper<LoginResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ResponseWrapper<LoginResponse>>> Login(LoginRequest request)
        {
            var result = await _authService.Login(request);

            if (!result.IsSuccessful)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
