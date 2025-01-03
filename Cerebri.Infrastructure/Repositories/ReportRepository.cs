using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace Cerebri.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ReportRepository> _logger;
        
        public ReportRepository(AppDbContext context, ILogger<ReportRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ReportModel> GetByIdAsync(Guid reportId)
        {
            return await _context.Reports.FindAsync(reportId) ?? throw new ArgumentException($"Report with id: {reportId} does not exist");
        }

        public async Task<IEnumerable<ReportModel?>> GetByUserIdAsync(Guid userId)
        {
            var existingUser = await _context.Users.FindAsync(userId) ?? throw new ArgumentException("User does not exist");

            var reports = await _context.Reports
                .Where(r => r.UserId == userId)
                .ToListAsync();

            if (reports != null) return reports;

            // Return an empty list if user has no entries saved
            return new List<ReportModel>();
        }

        public async Task InsertAsync(ReportModel reportModel)
        {
            var existingUser = await _context.Users.FindAsync(reportModel.UserId) ?? throw new ArgumentException("User does not exist");

            await _context.Reports.AddAsync(reportModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReportModel updatedReport)
        {
            var existingReport = await _context.Reports.FindAsync(updatedReport.Id) ?? throw new ArgumentException("Report does not exist");

            // Can only update the report name
            existingReport.ReportName = updatedReport.ReportName;
            _context.Reports.Update(existingReport);
            await _context.SaveChangesAsync();
        }
    }
}
