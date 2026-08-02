namespace BarberHub.Domain.Entities;

public class Image: BaseEntity
{
    public string ImageUrl { get; private set; } = null!;
    public string? Caption { get; private set; }
    
    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;
    
    public long? BarberId { get; private set; }
    public Barber? Barber { get; private set; } 
}