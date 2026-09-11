namespace BarberHub.Domain.Exceptions;

public class FileSizeLimitExceededException() : Exception("File size exceeds the maximum allowed limit.")
{
}