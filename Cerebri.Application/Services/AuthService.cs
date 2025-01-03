using Cerebri.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Cerebri.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string GenerateToken(string userId, string email)
        {
            var claims = new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            };
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string?> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !ValidatePassword(password, user.HashedPassword))
                return null;

            return GenerateToken(user.Id.ToString(), user.Email);
        }

        private bool ValidatePassword(string password, string hashedPassword)
        {
           return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public Guid GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("userId");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User Id claim not found");
            return Guid.Parse(userIdClaim.Value);
        }
    }
}
