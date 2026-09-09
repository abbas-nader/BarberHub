namespace BarberHub.Api.Contracts.WorkSchedule;

public record UpdateWorkScheduleRequest(
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek
);