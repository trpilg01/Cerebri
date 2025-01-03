
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
            try
            {
                var existingCheckIn = await _context.CheckIns
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync();

                if (existingCheckIn == null)
                {
                    throw new Exception("Check In does not exist");
                }

                _context.Remove(existingCheckIn);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<CheckInModel?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.CheckIns
                    .Where(x => x.Id == id)
                    .Include(x => x.MoodTags)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<IEnumerable<CheckInModel?>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.CheckIns
                    .Where(x => x.UserId == userId)
                    .Include(x => x.MoodTags)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
        

        public async Task InsertAsync(CheckInModel checkIn)
        {
            try
            {
                await _context.CheckIns.AddAsync(checkIn);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException?.Message ?? ex.Message);
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public Task UpdateAsync(CheckInModel checkIn)
        {
            throw new NotImplementedException();
        }
    }
}
