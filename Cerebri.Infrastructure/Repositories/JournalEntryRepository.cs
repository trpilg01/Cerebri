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
                if (entry != null)
                    _context.JournalEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        public async Task UpdateAsync(JournalEntryModel journalEntry)
        {
            try
            {
                var existingEntry = await _context.JournalEntries
                    .Include(e => e.MoodTags)
                    .FirstOrDefaultAsync(e => e.Id == journalEntry.Id);

                if (existingEntry == null)
                {
                    throw new Exception($"Journal entry with ID {journalEntry.Id} does not exist");
                }

                existingEntry.Title = journalEntry.Title;
                existingEntry.Content = journalEntry.Content;
                existingEntry.UpdatedAt = DateTime.UtcNow;

                if (journalEntry.MoodTags != null)
                {
                    existingEntry.MoodTags.Clear();

                    foreach (var newTag in journalEntry.MoodTags)
                    {
                        if (!existingEntry.MoodTags.Any(x => x.MoodId == newTag.MoodId))
                        {
                            existingEntry.MoodTags.Add(newTag);
                        }
                    }
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
