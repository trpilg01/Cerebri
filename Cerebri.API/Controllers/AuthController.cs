using Cerebri.API.DTOs;
using Cerebri.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cerebri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            try
            {
                var token = await _authService.AuthenticateUser(request.Email, request.Password);
                if (token == null)
                {
                    _logger.LogInformation($"Login request failed: {request.Email}");
                    return BadRequest("Could not validate credentials");
                }

                return Ok(new TokenResponseDTO(token));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
