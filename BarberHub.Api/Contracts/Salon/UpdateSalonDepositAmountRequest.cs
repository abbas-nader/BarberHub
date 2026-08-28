using BarberHub.Domain.Enums;

namespace BarberHub.Api.Contracts.Salon;

public record UpdateSalonDepositAmountRequest(
    decimal DepositAmountValue,
    Currency DepositAmountCurrency
    );