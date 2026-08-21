using BarberHub.Api.Contracts.Barber;
using BarberHub.Application.DTOs;

namespace BarberHub.Api.Mappers;

public static class BarberContractMapper
{
    public static BarberResponse ToResponse(this BarberDto dto) =>
        new(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.MobileNumber,
            dto.Description,
            dto.IsActive
        );

    public static CreateBarberRequest ToDto(this CreateBarberDto dto) =>
        new(
            dto.FirstName,
            dto.LastName,
            dto.MobileNumber,
            dto.Username,
            dto.Password,
            dto.Description,
            dto.SalonId
        );

    public static UpdateBarberRequest ToDto(this UpdateBarberDto dto) =>
        new(
            dto.FirstName,
            dto.LastName,
            dto.MobileNumber,
            dto.Username,
            dto.Password,
            dto.Description
        );
}