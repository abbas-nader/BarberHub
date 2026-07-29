using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class BarberService : BaseEntity
{
    public long  BarberId { get; private set; }
    public Barber Barber { get; private set; } = null!;
    
    public long ServiceId { get; private set; }
    public Service Service { get; private set; } = null!;

    public Money Money { get; private set; } = null!;
}