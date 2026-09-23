namespace BarberHub.Domain.Exceptions;

public class SmsSendFailedException() : Exception("Failed to send SMS. Please try again later.")
{
}