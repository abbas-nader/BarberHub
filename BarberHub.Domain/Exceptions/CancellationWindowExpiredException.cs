namespace BarberHub.Domain.Exceptions;

public class CancellationWindowExpiredException()
    : Exception("Appointment cannot be cancelled less than 12 hours before its start time.")
{
}