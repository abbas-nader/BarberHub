namespace BarberHub.Api.Contracts.WorkSchedule;

public record WorkScheduleResponse(
    long Id,
    TimeOnly StartTime,
    TimeOnly EndTime,
    DayOfWeek DayOfWeek,
    long BarberId
    );