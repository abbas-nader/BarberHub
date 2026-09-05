using BarberHub.Api.Contracts.BarberService;
using BarberHub.Application.DTOs.BarberService;

namespace BarberHub.Api.Mappers;

public static class BarberServiceContractMapper
{
    public static BarberServiceResponse ToResponse(this BarberServiceDto serviceDto)
        => new(
            serviceDto.Id,
            serviceDto.BarberId,
            serviceDto.ServiceId,
            serviceDto.PriceValue,
            serviceDto.PriceCurrency,
            serviceDto.Duration
        );

    public static CreateBarberServiceDto ToDto(this CreateBarberServiceRequest serviceDto)
        => new(
            serviceDto.BarberId,
            serviceDto.ServiceId,
            serviceDto.PriceValue,
            serviceDto.PriceCurrency,
            serviceDto.Duration
        );
    
    public static UpdateBarberServiceDto ToDto(this UpdateBarberServiceRequest serviceDto)
        => new(
            serviceDto.PriceValue,
            serviceDto.PriceCurrency,
            serviceDto.Duration
        );
}