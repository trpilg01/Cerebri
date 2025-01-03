using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface ICheckInRepository
    {
        Task InsertAsync(CheckInModel checkIn);
        Task<IEnumerable<CheckInModel?>> GetByUserIdAsync(Guid userId);
        Task<CheckInModel?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(CheckInModel checkIn);
    }
}
