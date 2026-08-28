using BarberHub.Api.Contracts.Salon;
using BarberHub.Application.DTOs.Salon;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Api.Mappers;

public static class SalonContractMapper
{
    public static SalonResponse ToResponse(this SalonDto salonDto)
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

    public static CreateSalonDto ToDto(this CreateSalonRequest request)
        => new(
            request.Name,
            request.Address,
            request.City,
            request.PhoneNumber,
            request.DepositAmountValue,
            request.DepositAmountCurrency,
            request.Description
        );

    public static UpdateSalonDto ToDto(this UpdateSalonRequest request)
        => new(
            request.Name,
            request.Address,
            request.City,
            request.PhoneNumber,
            request.Description
        );

    public static UpdateSalonDepositAmountDto ToDto(this UpdateSalonDepositAmountRequest request)
        => new(
            request.DepositAmountValue,
            request.DepositAmountCurrency
        );
}