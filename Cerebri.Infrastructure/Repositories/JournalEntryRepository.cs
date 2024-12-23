using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cerebri.Infrastructure.Repositories
{
    public class JournalEntryRepository : IJournalEntryRepository
    {
        private readonly AppDbContext _context;

        public JournalEntryRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
