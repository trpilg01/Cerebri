using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface IReportGenerator
    {
        Task<ReportModel> GenerateReport(List<JournalEntryModel> journals, Guid userId, MoodModel mostCommonMood, string reportName = "New Report");
        Task<OpenAIResponseModel?> GenerateReportInfo(List<JournalEntryModel> journals);
    }
}
