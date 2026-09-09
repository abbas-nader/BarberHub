namespace BarberHub.Api.Constants.WorkSchedule;

public static class WorkScheduleUriConstants
{
    private const string ControllerName = "workschedule";
    public const string GetAllByBarberId = $"{ControllerName}/{{barberId}}";
    public const string GetById = $"{ControllerName}/{{workscheduleId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update/{{workScheduleId}}";
    public const string Delete = $"{ControllerName}/delete/{{workScheduleId}}";
}