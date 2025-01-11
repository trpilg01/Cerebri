using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface IReportRepository
    {
        Task InsertAsync(ReportModel reportModel);
        Task<IEnumerable<ReportModel?>> GetByUserIdAsync(Guid userId);
        Task<ReportModel> GetByIdAsync(Guid reportId);
        Task UpdateAsync(ReportModel reportModel);
        Task DeleteAsync(Guid reportId);
    }
}
