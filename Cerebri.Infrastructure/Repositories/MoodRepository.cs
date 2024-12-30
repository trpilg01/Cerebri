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
    }
}
