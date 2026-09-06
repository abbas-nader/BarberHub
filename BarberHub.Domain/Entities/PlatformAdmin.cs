using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class PlatformAdmin : BaseUser
{
    private PlatformAdmin()
    {
    }

    public PlatformAdmin(string firstName, string lastName, string userName, string passwordHash, long creationBy) :
        base(firstName, lastName, userName, passwordHash)
    {
        Creation(creationBy);
    }
}