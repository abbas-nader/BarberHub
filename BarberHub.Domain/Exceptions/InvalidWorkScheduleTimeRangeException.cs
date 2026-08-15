namespace BarberHub.Domain.Exceptions;

public class InvalidWorkScheduleTimeRangeException()
    : Exception("End time must be greater than start time.")
{
}