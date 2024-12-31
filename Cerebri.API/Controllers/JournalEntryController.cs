using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cerebri.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntryController : ControllerBase
    {
        private readonly IJournalEntryService _journalEntryService;
        private readonly ILogger<JournalEntryController> _logger;
        private readonly IMapper _mapper;

        public JournalEntryController(IJournalEntryService journalEntryService, ILogger<JournalEntryController> logger, IMapper mapper)
        {
            _journalEntryService = journalEntryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalEntryDTO request)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Cannot find user");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                var entry = _mapper.Map<JournalEntryModel>(request);
                entry.UserId = userId;
                await _journalEntryService.CreateJournalEntryAsync(entry, request.Moods);
                return Ok("Journal Entry Created");
            }
            catch (Exception ex)    
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateJournalEntry([FromBody] UpdateJournalEntryDTO request)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Cannot find user");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                var entry = _mapper.Map<JournalEntryModel>(request);
                entry.UserId = userId;
                await _journalEntryService.UpdateJournalEntryAsync(entry, request.Moods);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserJournalEntries()
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Cannot find user");
            }

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                var entries = await _journalEntryService.GetJournalEntriesAsync(userId);
                return Ok(entries);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteJournalRequestDTO request)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                _logger.LogInformation("Invalid Token received");
                return Unauthorized("Invalid Token");
            }

            await _journalEntryService.DeleteJournalEntryAsync(request.EntryId);
            return Ok();
        }
    }
}
