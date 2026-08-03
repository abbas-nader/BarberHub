namespace BarberHub.Domain.Entities;

public class Review : BaseEntity
{
    public byte Rating { get; private set; }
    public string Comment { get; private set; } = null!;
    public bool IsApproved { get; private set; }
    public string? Reply { get; private set; } = null!;
    
    public long CustomerId { get; private set; }
    public long BarberId { get; private set; }
    public long AppointmentId { get; private set; }
    public long SalonId { get; private set; }
}