using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Infrastructure.Repositories
{
    public class MoodRepository : IMoodRepository
    {
        private readonly AppDbContext _context;

        public MoodRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoodModel>> GetMoods()
        {
            return await _context.Moods.ToListAsync();
        }

        public async Task<List<MoodModel>> GetMoodsByIdAsync(List<int> ids)
        {
            var moods = await _context.Moods.Select(x => x.Id).ToListAsync();

            // Check that every id provided is valid
            if (ids.Any(x => !moods.Contains(x)))
                throw new ArgumentException("Invalid mood ids provided");

            return await _context.Moods
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }
    }
}
