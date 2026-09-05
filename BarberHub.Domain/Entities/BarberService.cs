using BarberHub.Domain.Constants;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class BarberService : BaseEntity
{
    public long BarberId { get; private set; }
    public Barber Barber { get; private set; } = null!;

    public long ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;

    public Money Price { get; private set; } = null!;
    public TimeSpan Duration { get; private set; }

    private BarberService()
    {
    }

    public BarberService(long barberId,long serviceId, Money price, TimeSpan duration, long creationBy)
    {
        BarberId = barberId;
        ServiceId = serviceId;
        ValidateDuration(duration);
        Price = price ?? throw new RequiredFieldException(nameof(price));
        Duration = duration;
        Creation(creationBy);
    }

    public void Update(Money money , TimeSpan duration, long modifiedBy)
    {
        Price = money ?? throw new RequiredFieldException(nameof(money));
        ValidateDuration(duration);
        Duration = duration;
        Modified(modifiedBy);
    }
    private static void ValidateDuration(TimeSpan duration)
    {
        if (duration.Ticks < BarberServiceConstants.DurationMinValue)
            throw new InvalidServiceDurationException();
    }
}