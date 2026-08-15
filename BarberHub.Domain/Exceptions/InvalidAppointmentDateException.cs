namespace BarberHub.Domain.Exceptions;

public class InvalidAppointmentDateException() : Exception("Appointment date cannot be in the past.")
{
}