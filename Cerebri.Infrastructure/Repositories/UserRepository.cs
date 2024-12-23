using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cerebri.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(UserModel newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<UserModel?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
