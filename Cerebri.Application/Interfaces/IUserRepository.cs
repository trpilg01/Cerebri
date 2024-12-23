using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface IUserRepository
    {
        Task InsertAsync(UserModel newUser);
        Task<UserModel?> GetByIdAsync(Guid id);
        Task<UserModel?> GetByEmailAsync(string email);
        Task DeleteAsync(Guid id);
    }
}
