using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.Application.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IJournalEntryRepository _journalEntryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMoodRepository _moodRepository;

        public JournalEntryService(IJournalEntryRepository journalEntryRepository, IMoodRepository moodRepository, IUserRepository userRepository)
        {
            _journalEntryRepository = journalEntryRepository;
            _moodRepository = moodRepository;
            _userRepository = userRepository;
        }

        public async Task CreateJournalEntryAsync(JournalEntryModel journalEntry, List<MoodModel> moods, Guid userId)
        {
            foreach (MoodModel mood in moods)
            {
                journalEntry.MoodTags.Add(new JournalEntryMoodModel(journalEntry.Id, mood.Id));
            }
            journalEntry.UserId = userId;
            await _journalEntryRepository.InsertAsync(journalEntry);
        }

        public async Task DeleteJournalEntryAsync(Guid entryId)
        {
            await _journalEntryRepository.DeleteAsync(entryId);
        }

        public async Task<IEnumerable<JournalEntryResponseModel?>> GetJournalEntriesAsync(Guid userId)
        {
            List<JournalEntryResponseModel> responseModels = new List<JournalEntryResponseModel>();
            var journals = await _journalEntryRepository.GetByUserIdAsync(userId);
            
            if (journals == null)
            {
                return new List<JournalEntryResponseModel>();
            }

            foreach (var journal in journals)
            {
                if (journal != null)
                {
                    JournalEntryResponseModel responseModel = new JournalEntryResponseModel();

                    responseModel.Id = journal.Id;
                    responseModel.Title = journal.Title;
                    responseModel.Content = journal.Content;
                    responseModel.CreatedAt = journal.CreatedAt;

                    var moodIds = journal?.MoodTags.Select(x => x.MoodId).ToList();
                    var moods = new List<MoodModel>();

                    if (moodIds != null && moodIds.Any())
                    {
                        moods = await _moodRepository.GetMoodsByIdAsync(moodIds);
                        responseModel.Moods = moods;
                    }

                    responseModels.Add(responseModel);
                }
            }
            return responseModels;
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