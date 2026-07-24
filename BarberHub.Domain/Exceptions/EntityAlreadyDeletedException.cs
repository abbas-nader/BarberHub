namespace BarberHub.Domain.Exceptions;

public class EntityAlreadyDeletedException() : Exception("Entity has already been deleted.")
{
}