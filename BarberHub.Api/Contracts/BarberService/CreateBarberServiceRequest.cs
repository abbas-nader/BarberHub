using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.BarberService;

public record CreateBarberServiceRequest(
    long BarberId,
    long ServiceId,
    decimal PriceValue,
    Currency PriceCurrency,
    TimeSpan Duration
);