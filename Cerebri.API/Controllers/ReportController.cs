using AutoMapper;
using Cerebri.API.DTOs;
using Cerebri.Application.Interfaces;
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
        private readonly IMapper _mapper;

        public ReportController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpPost("stream")]
        public async Task<IActionResult> StreamPdf([FromBody] Guid reportId)
        {
            var report = await _reportService.GetById(reportId);
            if (report == null)
            {
                return BadRequest($"Could not find report with ID: {reportId}");
            }
            return File(report.ReportData, "application/pdf", report.ReportName);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.FindFirst("userId");
            if (userIdClaim == null)
            {
                return Unauthorized("Invalid token received");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            try
            {
                var reports = await _reportService.GetByUserId(userId);
                List<ReportResponseDTO> results = new List<ReportResponseDTO>();
                foreach(var report in reports)
                {
                    var reportResponse = _mapper.Map<ReportResponseDTO>(report);
                    results.Add(reportResponse);
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");
                if (userIdClaim == null)
                {
                    return Unauthorized("Invalid Token received");
                }
                var userId = Guid.Parse(userIdClaim.Value);
                var summary = await _reportService.GetSummary(userId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpGet("generate")]
        public async Task<IActionResult> CreateReport()
        {
            try
            {
                var userIdClaim = User.FindFirst("userId");
                if (userIdClaim == null)
                {
                    return Unauthorized("Invalid Token received");
                }
                var userId = Guid.Parse(userIdClaim.Value);
                var report = await _reportService.GenerateReport(userId);
                if (report == null)
                {
                    return Ok("could not generate report");
                }
                return File(report.ReportData, "application/pdf", report.ReportName);
            } catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
