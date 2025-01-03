using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface IReportService
    {
        Task<OpenAIResponseModel?> GetSummary(Guid userId);
        Task<IEnumerable<ReportModel?>> GetByUserId(Guid userId);
        Task<ReportModel> GetById(Guid id);
        Task<ReportModel> GenerateReport(Guid userId);
        Task UpdateReport(ReportModel report);
    }
}
