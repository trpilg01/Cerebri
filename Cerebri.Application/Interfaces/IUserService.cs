using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserModel newUser);
        Task DeleteUserAsync(Guid userId);
        Task UpdateUserAsync(UserModel updatedUser, Guid userId);
        Task<UserModel> GetUserByIdAsync(Guid userId);
    }
}
