using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public TimeSpan Duration { get; private set; }
    public Money Price { get; private set; } = null!;
    
    public long BarberId { get; private set; }
    public Barber Barber { get; private set; } = null!;
}