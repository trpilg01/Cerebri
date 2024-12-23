namespace Cerebri.API.DTOs
{
    public class TokenResponseDTO
    {
        public string Token { get; set; } = string.Empty;

        public TokenResponseDTO(string token)
        {
            Token = token;
        }
    }
}