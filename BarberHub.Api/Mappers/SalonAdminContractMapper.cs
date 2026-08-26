using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Application.DTOs.SalonAdmin;

namespace BarberHub.Api.Mappers;

public static class SalonAdminContractMapper
{
    public static SalonAdminResponse ToResponse(this SalonAdminDto dto)
        => new(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.MobileNumber
        );

    public static CreateSalonAdminDto ToDto(this CreateSalonAdminRequest request)
        => new(
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password,
            request.MobileNumber,
            request.SalonId
        );

    public static UpdateSalonAdminDto ToDto(this UpdateSalonAdminRequest request)
        => new(
            request.Id,
            request.FirstName,
            request.LastName,
            request.Username,
            request.Password,
            request.MobileNumber
        );

}