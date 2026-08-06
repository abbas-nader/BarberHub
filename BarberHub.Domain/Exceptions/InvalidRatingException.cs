namespace BarberHub.Domain.Exceptions;

public class InvalidRatingException() : Exception("Rating is invalid. Rating must be between 0 and 5.")
{
}