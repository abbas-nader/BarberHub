using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public DateTimeOffset? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public long? DeletedBy { get; private set; }
    public bool IsDeleted { get; private set; }

    protected BaseEntity()
    {
    }

    public void Creation(long createdBy)
    {
        if (createdBy < 0) throw new InvalidAuditUserIdException(nameof(createdBy));
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
    }

    public void Modified(long userId)
    {
        if (userId < 0) throw new InvalidAuditUserIdException(nameof(userId));
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }

    public void SoftDelete(long userId)
    {
        if(userId < 0) throw new InvalidAuditUserIdException(nameof(userId));
        if (IsDeleted) throw new EntityAlreadyDeletedException();
        DeletedAt = DateTimeOffset.UtcNow;
        DeletedBy = userId;
        IsDeleted = true;
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }
}