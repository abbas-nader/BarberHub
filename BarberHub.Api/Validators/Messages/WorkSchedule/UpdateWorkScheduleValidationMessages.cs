namespace BarberHub.Api.Validators.Messages.WorkSchedule;

public static class UpdateWorkScheduleValidationMessages
{
    public const string TimeRangeInvalid = "End time must be greater than start time.";

    public const string DayOfWeekInvalid = "Day of week is invalid.";

    public const string BarberIdInvalid = "Barber id must be a positive number.";
}