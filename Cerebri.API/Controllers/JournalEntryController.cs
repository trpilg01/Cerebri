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
        private readonly IAuthService _authService;

        public JournalEntryController(IJournalEntryService journalEntryService, ILogger<JournalEntryController> logger, IMapper mapper, IAuthService authService)
        {
            _journalEntryService = journalEntryService;
            _logger = logger;
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalEntryDTO request)
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var entry = _mapper.Map<JournalEntryModel>(request);
                await _journalEntryService.CreateJournalEntryAsync(entry, request.Moods, userId);
                return Ok("Journal Entry Created");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)    
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateJournalEntry([FromBody] UpdateJournalEntryDTO request)
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                var entry = _mapper.Map<JournalEntryModel>(request);
                entry.UserId = userId;
                await _journalEntryService.UpdateJournalEntryAsync(entry, request.Moods);
                return Ok();
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
        public async Task<IActionResult> GetUserJournalEntries()
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                var entries = await _journalEntryService.GetJournalEntriesAsync(userId);
                return Ok(entries);
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
        public async Task<IActionResult> Delete([FromBody] DeleteJournalRequestDTO request)
        {
            try
            {
                Guid userId = _authService.GetUserIdFromClaims(User);
                await _journalEntryService.DeleteJournalEntryAsync(request.EntryId);
                return Ok("Journal entry deleted");
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
