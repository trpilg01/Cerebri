
using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cerebri.Infrastructure.Repositories
{
    public class CheckInRepository : ICheckInRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CheckInRepository> _logger;

        public CheckInRepository(AppDbContext context, ILogger<CheckInRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(Guid id)
        {
            var existingCheckIn = await _context.CheckIns
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

            if (existingCheckIn == null)
                throw new ArgumentException("CheckIn does not exist");

            _context.Remove(existingCheckIn);
            await _context.SaveChangesAsync();
        }

        public async Task<CheckInModel> GetByIdAsync(Guid id)
        {
            var checkIn = await _context.CheckIns
                .Where(x => x.Id == id)
                .Include(x => x.MoodTags)
                .FirstOrDefaultAsync();
            if (checkIn == null)
                throw new ArgumentException("CheckIn does not exist");
            return checkIn;
        }

        public async Task<IEnumerable<CheckInModel?>> GetByUserIdAsync(Guid userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
                throw new ArgumentException("User does not exist");

            return await _context.CheckIns
                .Where(x => x.UserId == userId)
                .Include(x => x.MoodTags)
                .ToListAsync();
        }

        public async Task InsertAsync(CheckInModel checkIn)
        {
            var existingUser = await _context.Users.FindAsync(checkIn.UserId);
            if (existingUser == null)
                throw new ArgumentException("User does not exist");
            await _context.CheckIns.AddAsync(checkIn);
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(CheckInModel checkIn)
        {
            throw new NotImplementedException();
        }
    }
}
