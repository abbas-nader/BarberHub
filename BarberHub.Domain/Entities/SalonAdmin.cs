namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public long SalonId { get; set; }
    public Salon Salon { get; set; } = null!;
}