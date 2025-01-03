using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportGenerator _generator;
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportGenerator generator, IJournalEntryRepository journalEntryRepository, IReportRepository reportRepository)
        {
            _generator = generator;
            _journalEntryRepository = journalEntryRepository;
            _reportRepository = reportRepository;
        }

        public async Task<ReportModel?> GenerateReport(Guid userId)
        {
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId);
            var filteredJournals = journals.Where(x => x?.Content.Length > 0).ToList();
            var report = await _generator.GenerateReport(filteredJournals);
            return report;
        }

        public async Task<ReportModel?> GetById(Guid id)
        {
            return await _reportRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ReportModel?>> GetByUserId(Guid userId)
        {
            return await _reportRepository.GetByUserIdAsync(userId);
        }

        public async Task<OpenAIResponseModel?> GetSummary(Guid userId)
        {
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId);
            if (journals != null && journals.Any(x => x != null))
            {
                var filteredJournals = journals.Where(x => x?.Content.Length > 0).ToList();
                return await _generator.GenerateReportInfo(filteredJournals);
            }
            return null;
        }
    }
}
