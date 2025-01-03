using System.Security.Claims;

namespace Cerebri.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(string userId, string email);
        Task<string?> AuthenticateUser(string userId, string email);
        Guid GetUserIdFromClaims(ClaimsPrincipal user);
    }
}
