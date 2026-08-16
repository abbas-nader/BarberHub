using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class BarberService : BaseEntity
{
    public long BarberId { get; private set; }
    public Barber Barber { get; private set; } = null!;

    public long ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;

    public Money Money { get; private set; } = null!;

    private BarberService()
    {
    }

    public BarberService(long barberId,long serviceId, Money money, long creationBy)
    {
        BarberId = barberId;
        ServiceId = serviceId;
        Money = money ?? throw new RequiredFieldException(nameof(money));
        Creation(creationBy);
    }

    public void Update(Money money , long modifiedBy)
    {
        Money = money ?? throw new RequiredFieldException(nameof(Money));
        Modified(modifiedBy);
    }
}