using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cerebri.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoodController : ControllerBase
    {
        private readonly IMoodService _moodService;
        private readonly ILogger<MoodController> _logger;

        public MoodController(IMoodService moodService, ILogger<MoodController> logger)
        {
            _moodService = moodService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var moods = await _moodService.GetMoods();
                return Ok(moods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
