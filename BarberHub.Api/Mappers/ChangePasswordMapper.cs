using BarberHub.Api.Contracts.Shared;
using BarberHub.Application.DTOs.Shared;

namespace BarberHub.Api.Mappers;

public static class ChangePasswordMapper
{
    public static ChangePasswordDto ToDto(this ChangePasswordRequest request)
        => new(
            request.OldPassword,
            request.NewPassword
        );
}