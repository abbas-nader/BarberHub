using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.BarberService;

public record CreateBarberServiceDto(
    long BarberId,
    long ServiceId,
    decimal PriceValue,
    Currency Currency,
    TimeSpan Duration
);