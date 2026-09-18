using BarberHub.Api.Contracts.EndUser;
using BarberHub.Application.DTOs.EndUser;

namespace BarberHub.Api.Mappers;

public static class EndUserContractMapper
{
    public static EndUserResponse ToResponse(this EndUserDto dto)
        => new(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.UserName,
            dto.MobileNumber,
            dto.IsMobileVerified
        );

    public static UpdateEndUserDto ToDto(this UpdateEndUserRequest request)
        => new(
            request.FirstName,
            request.LastName,
            request.UserName,
            request.MobileNumber
        );
}