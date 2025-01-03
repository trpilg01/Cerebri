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

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
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

        [HttpGet("reports")]
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
