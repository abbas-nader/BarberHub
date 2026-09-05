using BarberHub.Api.Contracts.Service;
using BarberHub.Application.DTOs.Service;

namespace BarberHub.Api.Mappers;

public static class ServiceContractMapper
{
    public static ServiceResponse ToResponse(this ServiceDto serviceDto)
        => new(
            serviceDto.Id,
            serviceDto.Name,
            serviceDto.Description
        );

    public static CreateServiceDto ToDto(this CreateServiceRequest request)
        => new(
            request.Name,
            request.Description
        );

    public static UpdateServiceDto ToDto(this UpdateServiceRequest request)
        => new(
            request.Name,
            request.Description
        );
}