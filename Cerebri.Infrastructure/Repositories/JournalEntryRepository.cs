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
            try
            {
                var entry = await _context.JournalEntries.FindAsync(id);
                
                if (entry == null)
                {
                    throw new Exception("Entry to be deleted does not exist");
                }

                _context.JournalEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<JournalEntryModel?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.JournalEntries.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<IEnumerable<JournalEntryModel?>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.JournalEntries
                    .Where(x => x.UserId == userId)
                    .Include(x => x.MoodTags)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertAsync(JournalEntryModel journalEntry)
        {
            try
            {
                _context.JournalEntries.Add(journalEntry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }

        public async Task UpdateAsync(JournalEntryModel updatedEntry)
        {
            try
            {
                var existingEntry = await _context.JournalEntries
                    .Include(x => x.MoodTags)
                    .FirstOrDefaultAsync(x => x.Id == updatedEntry.Id);

                if (existingEntry == null)
                {
                    throw new Exception("Journal entry does not exist");
                }

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
