namespace BarberHub.Domain.Entities;

public class Barber : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public long SalonId { get; private set; }
    public IReadOnlyCollection<WorkSchedule> WorkSchedules { get; private set; } = null!;
    public IReadOnlyCollection<BarberService> BarberServices { get; private set; } = null!;
    public IReadOnlyCollection<Appointment> Appointments { get; private set; } = null!;
    public IReadOnlyCollection<Image> Images { get; private set; } = null!;
    public IReadOnlyCollection<Review> Reviews { get; private set; } = null!;
    
}