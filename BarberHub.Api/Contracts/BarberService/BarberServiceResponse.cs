using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.BarberService;

public record BarberServiceResponse(
    long Id,
    long BarberId,
    long ServiceId,
    decimal PriceValue,
    Currency Currency,
    TimeSpan Duration
);