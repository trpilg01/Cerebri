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
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<ReportController> _logger;

        public ReportController(IReportService reportService, IMapper mapper, IAuthService authService, ILogger<ReportController> logger)
        {
            _reportService = reportService;
            _mapper = mapper;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("stream")]
        public async Task<IActionResult> StreamPdf([FromBody] Guid reportId)
        {
            try
            {
                var report = await _reportService.GetById(reportId);
                return File(report.ReportData, "application/pdf", report.ReportName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var reports = await _reportService.GetByUserId(userId);
                List<ReportResponseDTO> results = new List<ReportResponseDTO>();
                foreach(var report in reports)
                {
                    var reportResponse = _mapper.Map<ReportResponseDTO>(report);
                    results.Add(reportResponse);
                }
                return Ok(results);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var summary = await _reportService.GetSummary(userId);
                return Ok(summary);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("generate")]
        public async Task<IActionResult> CreateReport()
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                var report = await _reportService.GenerateReport(userId);
                return File(report.ReportData, "application/pdf", report.ReportName);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] ReportResponseDTO updatedReport)
        {
            try
            {
                var userId = _authService.GetUserIdFromClaims(User);
                ReportModel reportModel = _mapper.Map<ReportModel>(updatedReport);
                await _reportService.UpdateReport(reportModel);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex.Message, ex);
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
