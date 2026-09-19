using BarberHub.Api.Contracts.PlatformAdmin;
using BarberHub.Application.DTOs.PlatformAdmin;

namespace BarberHub.Api.Mappers;

public static class PlatformAdminContractMapper
{
    public static PlatformAdminResponse ToResponse(this PlatformAdminDto dto)
        => new(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.UserName
        );

    public static CreatePlatformAdminDto ToDto(this CreatePlatformAdminRequest request)
        => new(
            request.FirstName,
            request.LastName,
            request.UserName,
            request.Password
        );

    public static UpdatePlatformAdminDto ToDto(this UpdatePlatformAdminRequest request)
        => new(
            request.FirstName,
            request.LastName,
            request.UserName
        );
}