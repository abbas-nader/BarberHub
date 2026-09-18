using BarberHub.Api.Contracts.User;
using BarberHub.Application.DTOs.User;

namespace BarberHub.Api.Mappers;

public static class UserContractsMapper
{
    public static UserResponse ToResponse(this UserDto dto)
        => new(
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.UserName,
            dto.MobileNumber,
            dto.IsMobileVerified
        );
}