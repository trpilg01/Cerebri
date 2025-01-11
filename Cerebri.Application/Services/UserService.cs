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
                throw new ArgumentException("Email is already registered. Please log in");

            // Hash password before saving to db
            newUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.HashedPassword);
            
            await _userRepository.InsertAsync(newUser);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var existingUser = await _userRepository.GetByIdAsync(userId) ?? throw new ArgumentException("This user id does not belong to any user");
            await _userRepository.DeleteAsync(existingUser);
        }

        public async Task UpdateUserAsync(UserModel updatedUser, Guid userId)
        {
            updatedUser.Id = userId;
            await _userRepository.UpdateAsync(updatedUser);
        }

        public async Task<UserModel> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetByIdAsync(userId) ?? throw new ArgumentException("User does not exist");
        }
    }
}
