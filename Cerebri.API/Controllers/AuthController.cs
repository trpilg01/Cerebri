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

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            try
            {
                var token = await _authService.AuthenticateUser(request.Email, request.Password);
                if (token == null)
                    return BadRequest("Could not validate credentials");

                return Ok(new TokenResponseDTO(token));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
