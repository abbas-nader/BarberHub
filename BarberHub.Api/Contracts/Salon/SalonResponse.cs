using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.Salon;

public record SalonResponse(
    long Id,
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    decimal DepositAmountValue,
    Currency DepositAmountCurrency,
    string? Description,
    bool IsActive
);