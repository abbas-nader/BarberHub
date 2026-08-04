namespace BarberHub.Domain.Exceptions;

public class CaptionLengthExceededException() : Exception("The caption must not exceed 500 characters.")
{
}