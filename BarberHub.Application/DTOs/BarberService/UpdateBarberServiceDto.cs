using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.BarberService;

public record UpdateBarberServiceDto(
    decimal PriceValue,
    Currency Currency,
    TimeSpan Duration
);