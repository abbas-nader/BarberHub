namespace BarberHub.Application.DTOs.WorkSchedule;

public record WorkScheduleDto(
    long Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek,
    long BarberId
    );