using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.Application.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _journalEntryRepository;

        public JournalEntryService(IJournalEntryRepository journalEntryRepository)
        {
            _journalEntryRepository = journalEntryRepository;
        }

        public async Task CreateJournalEntryAsync(JournalEntryModel journalEntry)
        {
            await _journalEntryRepository.InsertAsync(journalEntry);
        }

        public async Task DeleteJournalEntryAsync(Guid entryId)
        {
            await _journalEntryRepository.DeleteAsync(entryId);
        }

        public async Task<IEnumerable<JournalEntryModel?>> GetJournalEntriesAsync(Guid userId)
        {
            return await _journalEntryRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateJournalEntryAsync(JournalEntryModel updatedEntry, List<MoodModel> moods)
        {
            foreach (var mood in moods)
            {
                updatedEntry.MoodTags.Add(new JournalEntryMoodModel(updatedEntry.Id, mood.Id));
            }

            await _journalEntryRepository.UpdateAsync(updatedEntry);
        }
    }
}