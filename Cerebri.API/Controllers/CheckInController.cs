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
    [Authorize]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        private readonly ICheckInService _checkInService;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckInController> _logger;

        public CheckInController(ICheckInService checkInService, ILogger<CheckInController> logger, IMapper mapper)
        {
            _checkInService = checkInService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCheckInDTO request)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Invalid token. Please login");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            CheckInModel checkIn = _mapper.Map<CheckInModel>(request);
            checkIn.UserId = userId;
            await _checkInService.CreateCheckInAsync(checkIn);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Invalid token. Please login");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            var checkIns = await _checkInService.GetCheckInByUserIdAsync(userId);
            return Ok(checkIns);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] CheckInDeleteRequestDTO checkIn)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Invalid token. Please login");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            await _checkInService.DeleteCheckInAsync(checkIn.Id);
            return Ok(new { message = "CheckIn deleted"});
        }
    }
}
