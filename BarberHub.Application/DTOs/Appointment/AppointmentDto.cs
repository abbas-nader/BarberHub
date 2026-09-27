using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.Appointment;

public record AppointmentDto(
    long Id,
    DateOnly AppointmentDate,
    TimeOnly StartTime,
    TimeOnly EndTime,
    AppointmentStatus AppointmentStatus,
    string ServiceName,
    TimeSpan ServiceDuration,
    decimal ServicePriceValue,
    Currency ServicePriceCurrency,
    decimal DepositAmountValue,
    Currency DepositAmountCurrency,
    DepositPaymentMethod DepositPaymentMethod,
    DepositStatus DepositStatus,
    DateTimeOffset? CancelledAt,
    NoShowDetectionType? NoShowDetectionType,
    long BarberId,
    long UserId,
    long SalonId,
    long BarberServiceId
);