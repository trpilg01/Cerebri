using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<ReportModel?> GetByIdAsync(Guid reportId)
        {
            return await _context.Reports.FindAsync(reportId);
        }

        public async Task<IEnumerable<ReportModel?>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Reports
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task InsertAsync(ReportModel reportModel)
        {
            try
            {
                await _context.Reports.AddAsync(reportModel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Task UpdateAsync(ReportModel reportModel)
        {
            throw new NotImplementedException();
        }
    }
}
