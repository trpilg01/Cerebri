using Cerebri.Application.Interfaces;
using Cerebri.Domain.Entities;


namespace Cerebri.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserModel newUser)
        {
            var existingUser = await _userRepository.GetByEmailAsync(newUser.Email);
            
            if (existingUser != null)
                throw new Exception("Email is already registered. Please log in");

            // Hash password before saving to db
            newUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.HashedPassword);
            
            await _userRepository.InsertAsync(newUser);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);

            if (existingUser == null)
                throw new Exception("This User ID does not belong to any user.");

            await _userRepository.DeleteAsync(existingUser);
        }

        public async Task UpdateUserAsync(Guid userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId);

            if (existingUser == null)
                throw new Exception("This User ID does not belong to any user.");

            await _userRepository.UpdateAsync(existingUser);
        }
    }
}
