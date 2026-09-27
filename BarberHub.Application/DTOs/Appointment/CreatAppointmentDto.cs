using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.DTOs.Appointment;

public record CreatAppointmentDto(
    DateOnly AppointmentDate,
    TimeOnly StartTime,
    DepositPaymentMethod DepositPaymentMethod,
    long BarberServiceId
);