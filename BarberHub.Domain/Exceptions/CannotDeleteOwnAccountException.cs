namespace BarberHub.Domain.Exceptions;

public class CannotDeleteOwnAccountException() : Exception("You cannot delete your own account.")
{
}