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
    }
}
