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

        public async Task<ReportModel> GenerateReport(Guid userId)
        {
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId);
            var filteredJournals = journals.Where(x => x!= null && x?.Content.Length > 0).ToList();
            if (filteredJournals.Any())
            {
                var report = await _generator.GenerateReport(filteredJournals, userId);
                return report ?? throw new Exception();
            }
            throw new InvalidOperationException("There are not enough journal entries to generate a report");
        }

        public async Task<ReportModel> GetById(Guid id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
                throw new ArgumentException("Could not find a report with the provided Id");
            return report;
        }

        public async Task<IEnumerable<ReportModel?>> GetByUserId(Guid userId)
        {
            return await _reportRepository.GetByUserIdAsync(userId);
        }

        /// <summary>
        /// Requests an AI generated summary of all journal entries for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<OpenAIResponseModel?> GetSummary(Guid userId)
        {
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId);
            if (journals == null || journals.Count() == 0)
                throw new InvalidOperationException("User has no journal entries saved");
            return await _generator.GenerateReportInfo(journals.ToList());
        }

        public async Task UpdateReport(ReportModel updatedReport)
        {
            await _reportRepository.UpdateAsync(updatedReport);
        }
    }
}
