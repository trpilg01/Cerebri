using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cerebri.Infrastructure.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<JournalEntryRepository> _logger;

        public JournalEntryRepository(AppDbContext context, ILogger<JournalEntryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entry = await _context.JournalEntries.FindAsync(id);

            if (entry == null)
                throw new ArgumentException("Entry does not exist");

            _context.JournalEntries.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<JournalEntryModel> GetByIdAsync(Guid id)
        {
            return await _context.JournalEntries.FindAsync(id) ?? throw new ArgumentException("Journal entry does not exist");
        }

        /// <summary>
        /// Retrieves a user from the provided user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IEnumerable<JournalEntryModel>> GetByUserIdAsync(Guid userId)
        {
            var existingUser = await _context.Users.FindAsync(userId) ?? throw new ArgumentException("User does not exist");

            var journals = await _context.JournalEntries
                .Where(x => x.UserId == userId)
                .Include(x => x.MoodTags)
                .ToListAsync();

            if (journals == null)
                return new List<JournalEntryModel>();

            return journals;
        }

        public async Task InsertAsync(JournalEntryModel journalEntry)
        {
            var existingUser = await _context.Users.FindAsync(journalEntry.UserId) ?? throw new ArgumentException("User does not exist");

            _context.JournalEntries.Add(journalEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JournalEntryModel updatedEntry)
        {
            try
            {
                var existingEntry = await _context.JournalEntries
                    .Include(x => x.MoodTags)
                    .FirstOrDefaultAsync(x => x.Id == updatedEntry.Id);

                if (existingEntry == null)
                    throw new Exception("Journal entry does not exist");

                existingEntry.Title = updatedEntry.Title;
                existingEntry.Content = updatedEntry.Content;

                // JournalEntry Moods
                var journalEntryMoods = await _context.JournalEntryMoods
                    .Where(x => x.JournalEntryId == existingEntry.Id)
                    .ToListAsync();

                // Remove current and replace with new
                if (journalEntryMoods != null)
                {
                    _context.JournalEntryMoods.RemoveRange(journalEntryMoods);
                    _context.JournalEntryMoods.AddRange(updatedEntry.MoodTags);
                }

                existingEntry.MoodTags.Clear();

                foreach(var moodTag in updatedEntry.MoodTags)
                {
                    existingEntry.MoodTags.Add(moodTag);
                }

                _context.JournalEntries.Update(existingEntry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
