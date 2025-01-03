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
        private readonly IAuthService _authService;

        public CheckInController(ICheckInService checkInService, ILogger<CheckInController> logger, IMapper mapper, IAuthService authService)
        {
            _checkInService = checkInService;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCheckInDTO request)
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                CheckInModel checkIn = _mapper.Map<CheckInModel>(request);
                checkIn.UserId = userId;
                await _checkInService.CreateCheckIn(checkIn);
                return Ok("CheckIn created");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                var checkIns = await _checkInService.GetCheckInByUserId(userId);
                return Ok(checkIns);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] CheckInDeleteRequestDTO checkIn)
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                await _checkInService.DeleteCheckIn(checkIn.Id);
                return Ok("CheckIn deleted");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
