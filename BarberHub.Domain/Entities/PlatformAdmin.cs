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

    public new void Update(string firstName, string lastName, string userName, long modifiedBy)
    {
        base.Update(firstName, lastName, userName, modifiedBy);
    }

    public new void ChangePassword(string passwordHash, long modifiedBy)
    {
        base.ChangePassword(passwordHash, modifiedBy);
    }
}