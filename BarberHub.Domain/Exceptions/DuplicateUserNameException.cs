namespace BarberHub.Domain.Exceptions;

public class DuplicateUserNameException() : Exception("A user with this username already exists")
{
}