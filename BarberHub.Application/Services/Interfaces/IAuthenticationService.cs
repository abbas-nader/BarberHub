using BarberHub.Application.DTOs.Auth;
using BarberHub.Application.Security.Authentication;

namespace BarberHub.Application.Services.InterFaces;

public interface IAuthenticationService
{
    Task<TokenResult> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default);
    Task<TokenResult> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken = default);
    Task<TokenResult> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeAsync(string rawRefreshToken, CancellationToken cancellationToken = default);
}