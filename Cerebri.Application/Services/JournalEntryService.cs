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

        public async Task CreateJournalEntryAsync(JournalEntryModel journalEntry, List<eMoods> moodTags)
        {
            foreach (eMoods moodTag in moodTags)
            {
                journalEntry.MoodTags.Add(new JournalEntryMoodModel(moodTag));
            }

            await _journalEntryRepository.InsertAsync(journalEntry);
        }

        public async Task<IEnumerable<JournalEntryModel?>> GetJournalEntriesAsync(Guid userId)
        {
            return await _journalEntryRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdateJournalEntryAsync(JournalEntryModel journalEntry, List<eMoods> moodTags)
        {
            foreach (eMoods moodTag in moodTags)
            {
                journalEntry.MoodTags.Add(new JournalEntryMoodModel(moodTag));
            }

            await _journalEntryRepository.UpdateAsync(journalEntry);
        }
    }
}