using BarberHub.Api.Contracts.Salon;
using BarberHub.Application.DTOs.Salon;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Api.Mappers;

public static class SalonContractMapper
{
    public static SalonResponse ToResponse(SalonDto salonDto)
        => new(
            salonDto.Id,
            salonDto.Name,
            salonDto.Address,
            salonDto.City,
            salonDto.PhoneNumber,
            salonDto.DepositAmountValue,
            salonDto.DepositAmountCurrency,
            salonDto.Description,
            salonDto.IsActive
        );

    public static CreateSalonDto ToDto(CreateSalonRequest request)
        => new(
            request.Name,
            request.Address,
            request.City,
            request.PhoneNumber,
            request.DepositAmountValue,
            request.DepositAmountCurrency,
            request.Description
        );

    public static UpdateSalonDto ToDto(UpdateSalonRequest request)
        => new(
            request.Id,
            request.Name,
            request.Address,
            request.City,
            request.PhoneNumber,
            request.Description
        );
}