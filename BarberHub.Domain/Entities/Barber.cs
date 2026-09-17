using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Domain.Entities;

public class Barber : BaseEntity
{
    private readonly List<WorkSchedule> _workSchedules = [];
    private readonly List<BarberService> _barberServices = [];
    private readonly List<Appointment> _appointments = [];
    private readonly List<Gallery> _galleries = [];
    private readonly List<Review> _reviews = [];

    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    public string? Description { get; private set; }
    public bool IsActive { get; private set; }
    public long SalonId { get; private set; }

    public IReadOnlyCollection<WorkSchedule> WorkSchedules => _workSchedules.AsReadOnly();
    public IReadOnlyCollection<BarberService> BarberServices => _barberServices.AsReadOnly();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<Gallery> Galleries => _galleries.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private Barber()
    {
    }

    public Barber(string? description, long userId, long salonId, long creationBy)
    {
        ValidateDescription(description);
        Description = description;
        IsActive = true;
        UserId = userId;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void Update(string? description, long modifiedBy)
    {
        ValidateDescription(description);
        Description = description;
        Modified(modifiedBy);
    }

    public void Activate(long modifiedBy) { IsActive = true; Modified(modifiedBy); }
    public void Deactivate(long modifiedBy) { IsActive = false; Modified(modifiedBy); }
    
    private static void ValidateDescription(string? description)
    {
        if (description is { Length: > BarberConstants.DescriptionMaxLength })
            throw new InvalidBarberDescriptionException();
    }
}