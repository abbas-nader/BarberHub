namespace BarberHub.Domain.Exceptions;

public class InvalidAppointmentTimeRangeException() : Exception("Appointment end time must be after start time.")
{
}