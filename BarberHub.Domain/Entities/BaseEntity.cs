using BarberHub.Domain.Exceptions;

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
    public bool IsDeleted  => DeletedAt.HasValue;
    protected BaseEntity()
    {
    }

    public void Creation(long createdBy)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(createdBy);
        CreatedAt = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
    }

    public void Modified(long userId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }

    public void SoftDelete(long userId)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(userId);
        if (IsDeleted) throw new EntityAlreadyDeletedException();
        DeletedAt = DateTimeOffset.UtcNow;
        DeletedBy = userId;
        ModifiedAt = DateTimeOffset.UtcNow;
        ModifiedBy = userId;
    }
}