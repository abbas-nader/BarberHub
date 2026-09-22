using BarberHub.Api.Contracts.Auth;
using BarberHub.Application.DTOs.Auth;
using BarberHub.Application.Security.Authentication;

namespace BarberHub.Api.Mappers;

public static class AuthContractMapper
{
    public static LoginDto ToDto(this LoginRequest request)
        => new(
            request.Username,
            request.Password,
            request.Role
        );

    public static RegisterDto ToDto(this RegisterRequest request)
        => new(
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password,
            request.MobileNumber
        );

    public static TokenResponse ToResponse(this TokenResult tokenResult)
        => new(
            tokenResult.AccessToken,
            tokenResult.AccessTokenExpireAt,
            tokenResult.RefreshToken,
            tokenResult.RefreshTokenExpireAt
        );
}