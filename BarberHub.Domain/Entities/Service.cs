using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public TimeSpan Duration { get; private set; }
    
    public long SalonId { get; private set; }
}