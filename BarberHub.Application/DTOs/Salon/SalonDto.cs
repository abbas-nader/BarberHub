using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.DTOs.Salon;

public record SalonDto(
    long Id,
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    decimal DepositAmountValue,
    Currency DepositAmountCurrency,
    string? Description,
    bool IsActive);