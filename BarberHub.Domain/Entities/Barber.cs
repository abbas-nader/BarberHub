using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class Barber : BaseUser
{
    private readonly List<WorkSchedule> _workSchedules = [];
    private readonly List<BarberService> _barberServices = [];
    private readonly List<Appointment> _appointments = [];
    private readonly List<Gallery> _galleries = [];
    private readonly List<Review> _reviews = [];

    public string MobileNumber { get; private set; } = null!;
    public bool IsMobileVerified { get; private set; }
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

    public Barber(string firstName, string lastName, string mobileNumber, string userName, string passwordHash,
        string? description, long salonId, long creationBy)
        : base(firstName, lastName, userName, passwordHash)
    {
        ValidateMobileNumber(mobileNumber);
        MobileNumber = mobileNumber;
        IsMobileVerified = true;
        Description = description;
        IsActive = true;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string mobileNumber, string userName, string? description,
        long modifiedBy)
    {
        Update(firstName, lastName, userName, modifiedBy);
        ValidateMobileNumber(mobileNumber);
        MobileNumber = mobileNumber;
        Description = description;
    }

    public void Activate(long modifiedBy)
    {
        IsActive = true;
        Modified(modifiedBy);
    }

    public void Deactivate(long modifiedBy)
    {
        IsActive = false;
        Modified(modifiedBy);
    }

    public new void ChangePassword(string passwordHash, long modifiedBy)
    {
        base.ChangePassword(passwordHash, modifiedBy);
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }
}