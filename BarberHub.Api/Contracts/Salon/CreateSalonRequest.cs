using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.Salon;

public record CreateSalonRequest(
    string Name,
    string Address,
    string City,
    string PhoneNumber,
    decimal DepositAmountValue,
    Currency DepositAmountCurrency,
    string? Description
);