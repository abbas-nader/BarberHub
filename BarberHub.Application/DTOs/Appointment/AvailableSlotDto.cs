namespace BarberHub.Application.DTOs.Appointment;

public record AvailableSlotDto(
    TimeOnly StartTime,
    TimeOnly EndTime
);