using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Salon : BaseEntity
{
    private readonly List<Barber> _barbers = [];
    private readonly List<Appointment> _appointments = [];
    private readonly List<Service> _services = [];
    private readonly List<Gallery> _galleries = [];
    private readonly List<Review> _reviews = [];

    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public Money DepositAmount { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Barber> Barbers => _barbers.AsReadOnly();
    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
    public IReadOnlyCollection<Gallery> Galleries => _galleries.AsReadOnly();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
    public SalonAdmin SalonAdmin { get; private set; } = null!;

    private Salon()
    {
    }

    public Salon(string name, string address, string city, string phoneNumber, Money depositAmount,
        string? description, long creationBy)
    {
        ValidateName(name);
        ValidateAddress(address);
        ValidateCity(city);
        ValidatePhoneNumber(phoneNumber);
        ValidateDepositAmount(depositAmount);
        ValidateDescription(description);

        Name = name;
        Address = address;
        City = city;
        PhoneNumber = phoneNumber;
        DepositAmount = depositAmount;
        Description = description;
        IsActive = false;

        Creation(creationBy);
    }

    public void UpdateInfo(string name, string address, string city, string phoneNumber, string? description,
        long modifiedBy)
    {
        ValidateName(name);
        ValidateAddress(address);
        ValidateCity(city);
        ValidatePhoneNumber(phoneNumber);
        ValidateDescription(description);

        Name = name;
        Address = address;
        City = city;
        PhoneNumber = phoneNumber;
        Description = description;

        Modified(modifiedBy);
    }

    public void UpdateDepositAmount(Money depositAmount, long modifiedBy)
    {
        ValidateDepositAmount(depositAmount);
        DepositAmount = depositAmount;
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

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new RequiredFieldException(nameof(name));
    }

    private static void ValidateAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new RequiredFieldException(nameof(address));
    }

    private static void ValidateCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new RequiredFieldException(nameof(city));
    }

    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new RequiredFieldException(nameof(phoneNumber));
    }

    private static void ValidateDepositAmount(Money depositAmount)
    {
        if (depositAmount is null)
            throw new InvalidDepositAmountException();
    }

    private static void ValidateDescription(string? description)
    {
        if (description is { Length: > SalonConstants.DescriptionMaxLength })
            throw new InvalidSalonDescriptionException();
    }
}