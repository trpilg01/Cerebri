using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cerebri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper, IAuthService authService)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO request)
        {
            try
            {
                var newUser = _mapper.Map<UserModel>(request);
                await _userService.CreateUserAsync(newUser);
                return Ok("User created successfully.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Validation error occurred while creating user");
                return BadRequest("Validation error: " +  ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                await _userService.DeleteUserAsync(userId);
                return Ok("User deleted");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " +  ex.Message);
            }
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetUserInformation()
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var user = await _userService.GetUserByIdAsync(userId);
                return Ok(_mapper.Map<UserInfoDTO>(user));
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequestDTO request)
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var userModel = _mapper.Map<UserModel>(request);
                await _userService.UpdateUserAsync(userModel, userId);
                return Ok("User updated");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}