namespace BarberHub.Domain.Exceptions;

public class ReviewAlreadyApprovedException()
    : Exception("Review has already been approved and can no longer be edited.")
{
}