using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class Barber : BaseEntity
{
    private readonly List<WorkSchedule> _workSchedules = [];
    private readonly List<BarberService> _barberServices = [];
    private readonly List<Appointment> _appointments = [];
    private readonly List<Gallery> _Images = [];
    private readonly List<Review> _reviews = [];
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public long SalonId { get; private set; }
    public IReadOnlyCollection<WorkSchedule> WorkSchedules => _workSchedules.AsReadOnly();
    public IReadOnlyCollection<BarberService> BarberServices => _barberServices.AsReadOnly();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<Gallery> Images => _Images.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private Barber()
    {
    }

    public Barber(string firstName, string lastName, string mobileNumber, string userName, string passwordHash,
        string? description, long salonId, long creationBy)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateMobileNumber(mobileNumber);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        UserName = userName;
        PasswordHash = passwordHash;
        Description = description;
        IsActive = true;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string mobileNumber, string userName,
        string passwordHash,
        string? description, long modifiedBy)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateMobileNumber(mobileNumber);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        UserName = userName;
        PasswordHash = passwordHash;
        Description = description;
        Modified(modifiedBy);
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

    private static void ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new RequiredFieldException(nameof(firstName));
    }

    private static void ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new RequiredFieldException(nameof(lastName));
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }

    private static void ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new RequiredFieldException(nameof(userName));
    }

    private static void ValidatePasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new RequiredFieldException(nameof(passwordHash));
    }
}