using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.BarberService;

public record UpdateBarberServiceRequest(
    decimal PriceValue,
    Currency PriceCurrency,
    TimeSpan Duration
);