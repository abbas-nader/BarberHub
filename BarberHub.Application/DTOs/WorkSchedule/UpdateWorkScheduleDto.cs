namespace BarberHub.Application.DTOs.WorkSchedule;

public record UpdateWorkScheduleDto(
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek
);