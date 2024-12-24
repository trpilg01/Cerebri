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
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task CreateUser([FromBody] CreateUserDTO request)
        {
            var newUser = _mapper.Map<UserModel>(request);
            try
            {
                await _userService.CreateUserAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var userIdClaim = User.FindFirst("userId");

            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid token provided");
                return Unauthorized("Cannot find user");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                await _userService.DeleteUserAsync(userId);
                return Ok("User Deleted");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}