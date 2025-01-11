using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportGenerator _generator;
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ICheckInRepository _checkInRepository;
        private readonly IMoodRepository _moodRepository;
        public ReportService(IReportGenerator generator, 
            IJournalEntryRepository journalEntryRepository, ICheckInRepository checkInRepository, 
            IReportRepository reportRepository, IMoodRepository moodRepository)
        {
            _generator = generator;
            _journalEntryRepository = journalEntryRepository;
            _reportRepository = reportRepository;
            _checkInRepository = checkInRepository;
            _moodRepository = moodRepository;
        }

        /// <summary>
        /// Summarizes all journal entries by the user and creates an AI generated summary of them.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A report model containing the pdf data of the generated report</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<ReportModel> GenerateReport(Guid userId, DateTime startDate, DateTime endDate)
        {
            var journals = await GetRelevantJournals(userId, startDate, endDate);
            var checkIns = await GetRelevantCheckIns(userId, startDate, endDate);
            
            if (journals.Count < 3) throw new InvalidOperationException("Not enough valid journal entries to generate a report");

            var mostCommonMood = await GetMostCommonMood(checkIns, journals);
            string reportName = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}";

            return await _generator.GenerateReport(journals, userId, mostCommonMood, reportName);
        }
        
        /// <summary>
        /// Returns the journal entries that were created between the start date and end date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private async Task<List<JournalEntryModel>> GetRelevantJournals(Guid userId, DateTime startDate, DateTime endDate)
        {
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId) ?? new List<JournalEntryModel>();
            var filteredJournals = journals
                .Where(x => x.Content.Length > 50 && x.CreatedAt.Date >= startDate && x.CreatedAt <= endDate).ToList();

            return filteredJournals ?? new List<JournalEntryModel>();
        }

        private async Task<List<CheckInModel>> GetRelevantCheckIns(Guid userId, DateTime startDate, DateTime endDate)
        {
            var checkIns = await _checkInRepository.GetByUserIdAsync(userId);
            var filteredCheckIns = checkIns
                .Where(x => x.MoodTags.Count > 0 && x.CreatedAt.Date >= startDate && x.CreatedAt <= endDate).ToList();
            
            return filteredCheckIns ?? new List<CheckInModel>();
        }

        /// <summary>
        /// Retrieves the report with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ReportModel> GetById(Guid id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
                throw new ArgumentException("Could not find a report with the provided Id");
            return report;
        }

        /// <summary>
        /// Retrieves all reports with the corresponding user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the most common mood recorded by the user from their check-ins and journal entries
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<MoodModel> GetMostCommonMood(List<CheckInModel> checkIns, List<JournalEntryModel> journals)
        {
            List<int> moodIds = new List<int>();
            var journalMoodIds = journals.SelectMany(x => x.MoodTags.Select(y => y.MoodId)).ToList();
            moodIds.AddRange(journalMoodIds);
            
            var checkInMoodIds = checkIns.SelectMany(x => x.MoodTags.Select(y => y.MoodId)).ToList();
            moodIds.AddRange(journalMoodIds);

            var mostCommon = moodIds
                .GroupBy(n => n)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;

            return await _moodRepository.GetByIdAsync(mostCommon);
        }

        public async Task UpdateReport(ReportModel updatedReport)
        {
            await _reportRepository.UpdateAsync(updatedReport);
        }

        public async Task DeleteReport(Guid reportId)
        {
            await _reportRepository.DeleteAsync(reportId);
        }
    }
}
