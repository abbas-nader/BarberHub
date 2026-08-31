using BarberHub.Api.Contracts.Auth;
using BarberHub.Application.DTOs.Auth;
using BarberHub.Application.Security.JwtToken;

namespace BarberHub.Api.Mappers;

public static class AuthContractMapper
{
    public static LoginDto ToDto(this LoginRequest request)
        => new(
            request.Username,
            request.Password
        );

    public static TokenResponse ToResponse(this TokenResult tokenResult)
        => new(
            tokenResult.AccessToken,
            tokenResult.AccessTokenExpiresAt,
            tokenResult.RefreshToken,
            tokenResult.RefreshTokenExpiresAt
        );
}