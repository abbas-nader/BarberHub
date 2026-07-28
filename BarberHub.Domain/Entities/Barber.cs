namespace BarberHub.Domain.Entities;

public class Barber : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string? Description { get;private set; }
    public bool IsActive { get; private set; }

    
    public long SalonId { get;private set; }
    public Salon Salon { get; private set; } = null!;
}