using JwtAuthDotNet9.Entites;
using JwtAuthDotNet9.Models;

namespace JwtAuthDotNet9.Services
{
    public interface IAuthService
    {
        public Task<User?> RegisterAsync(UserDto request);
        public Task<TokenResponseDto?> LoginAsync(UserDto request);
        public Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
