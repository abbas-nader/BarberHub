namespace BarberHub.Domain.Exceptions;

public class ReviewNotApprovedException() : Exception("Cannot reply to a review that has not been approved yet.")
{
}