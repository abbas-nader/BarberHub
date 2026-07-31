namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public string FullName { get; private set; } = null!;
    public string UserName { get;private set; } = null!;
    public string PasswordHash { get;private set; } = null!;
    
    public long SalonId { get;private set; }
    public Salon Salon { get; private set; } = null!;
}