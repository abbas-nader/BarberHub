namespace BarberHub.Api.Contracts.WorkSchedule;

public record CreateWorkScheduleRequest(
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek,
    long BarberId
);