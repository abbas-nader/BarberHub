namespace BarberHub.Application.DTOs.WorkSchedule;

public record CreateWorkScheduleDto(
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek,
    long BarberId
);