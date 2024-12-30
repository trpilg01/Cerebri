using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface IJournalEntryRepository
    {
        Task InsertAsync(JournalEntryModel journalEntry);
        Task<IEnumerable<JournalEntryModel?>> GetByUserIdAsync(Guid userId);
        Task<JournalEntryModel?> GetByIdAsync(Guid id);
        Task UpdateAsync(JournalEntryModel journalEntry);
        Task DeleteAsync(Guid id);        
    }
}
