using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.BarberService;

public record BarberServiceDto(
    long Id,
    long BarberId,
    long ServiceId,
    decimal PriceValue,
    Currency Currency,
    TimeSpan Duration
    );