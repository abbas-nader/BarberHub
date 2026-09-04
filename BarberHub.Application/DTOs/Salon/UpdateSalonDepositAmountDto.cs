using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.Salon;

public record UpdateSalonDepositAmountDto(
    decimal DepositAmountValue,
    Currency Currency
);