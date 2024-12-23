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
        private readonly IMapper _mapper;

        public JournalEntryController(IJournalEntryService journalEntryService, IMapper mapper)
        {
            _journalEntryService = journalEntryService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateJournalEntry([FromBody] CreateJournalEntryDTO request)
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
                return Unauthorized("Cannot find user");

            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                var entry = _mapper.Map<JournalEntryModel>(request);
                entry.UserId = userId;
                await _journalEntryService.CreateJournalEntryAsync(entry, request.Moods);
                return Ok();
            }
            catch (Exception ex)    
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserJournalEntries()
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
                return Unauthorized("Cannot find user");

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
    }
}
