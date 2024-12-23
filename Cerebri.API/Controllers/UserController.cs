using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cerebri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
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
    }
}