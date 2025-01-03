using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface IReportGenerator
    {
        Task<ReportModel?> GenerateReport(List<JournalEntryModel?>? journals, Guid userId);
        Task<OpenAIResponseModel?> GenerateReportInfo(List<JournalEntryModel> journals);
    }
}
