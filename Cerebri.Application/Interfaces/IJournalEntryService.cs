﻿using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.Application.Interfaces
{
    public interface IJournalEntryService
    {
        Task CreateJournalEntryAsync(JournalEntryModel journalEntry, Guid userId);
        Task<IEnumerable<JournalEntryResponseModel?>> GetJournalEntriesAsync(Guid userId);
        Task UpdateJournalEntryAsync(JournalEntryModel journalEntry, List<MoodModel> Moods);
        Task DeleteJournalEntryAsync(Guid entryId);
    }
}
