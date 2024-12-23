using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.Application.Interfaces
{
    public interface IJournalEntryService
    {
        Task CreateJournalEntryAsync(JournalEntryModel journalEntry, List<eMoods> moodTags);
        Task<IEnumerable<JournalEntryModel?>> GetJournalEntriesAsync(Guid userId);
    }
}
