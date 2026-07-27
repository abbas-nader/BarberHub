namespace BarberHub.Domain.Entities;

public class Salon : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Description  { get; set; }
    public decimal DepositAmount  { get; set; }
    public bool IsActive { get; set; }
}