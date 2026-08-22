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

    public static CreateBarberDto ToDto(this CreateBarberRequest request) =>
        new(
            request.FirstName,
            request.LastName,
            request.MobileNumber,
            request.Username,
            request.Password,
            request.Description,
            request.SalonId
        );

    public static UpdateBarberDto ToDto(this UpdateBarberRequest request) =>
        new(
            request.Id,
            request.FirstName,
            request.LastName,
            request.MobileNumber,
            request.Username,
            request.Password,
            request.Description
        );
}