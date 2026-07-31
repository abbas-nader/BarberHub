using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Salon : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public Money DepositAmount { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<Barber> Barbers { get; private set; } = null!;
    public IReadOnlyCollection<Appointment> Appointments { get; private set; } = null!;
    
    // private Salon()
    // {
    // }
    //
    // public Salon(string name, string address, string city, string phoneNumber, Money depositAmount,
    //     string? description)
    // {
    //     ValidateName(name);
    //     ValidateAddress(address);
    //     ValidateCity(city);
    //     ValidatePhoneNumber(phoneNumber);
    //     ValidateDepositAmount(depositAmount);
    //     ValidateDescription(description);
    //     Name = name;
    //     Address = address;
    //     City = city;
    //     PhoneNumber = phoneNumber;
    //     DepositAmount = depositAmount;
    //     Description = description;
    //     IsActive = false;
    // }
    //
    // public void UpdateInfo(string name, string address, string city, string phoneNumber, string? description)
    // {
    //     ValidateName(name);
    //     ValidateAddress(address);
    //     ValidateCity(city);
    //     ValidatePhoneNumber(phoneNumber);
    //     ValidateDescription(description);
    //     Name = name;
    //     Address = address;
    //     City = city;
    //     PhoneNumber = phoneNumber;
    //     Description = description;
    // }
    //
    // public void UpdateDepositAmount(Money depositAmount)
    // {
    //     ValidateDepositAmount(depositAmount);
    //     DepositAmount = depositAmount;
    // }
    //
    // public void Activate() => IsActive = true;
    // public void Deactivate() => IsActive = false;
    //
    // private static void ValidateName(string name)
    // {
    //     if (string.IsNullOrWhiteSpace(name))
    //         throw new ArgumentException("Salon name cannot be null or empty", nameof(name));
    // }
    //
    // private static void ValidateAddress(string address)
    // {
    //     if (string.IsNullOrWhiteSpace(address))
    //         throw new ArgumentException("Salon address cannot be null or empty", nameof(address));
    // }
    //
    // private static void ValidateCity(string city)
    // {
    //     if (string.IsNullOrWhiteSpace(city))
    //         throw new ArgumentException("Salon city cannot be null or empty", nameof(city));
    // }
    //
    // private static void ValidatePhoneNumber(string phoneNumber)
    // {
    //     if (string.IsNullOrWhiteSpace(phoneNumber))
    //         throw new ArgumentException("Salon phone number cannot be null or empty", nameof(phoneNumber));
    // }
    //
    // private static void ValidateDepositAmount(Money depositAmount)
    // {
    //     ArgumentNullException.ThrowIfNull(depositAmount);
    // }
    //
    // private static void ValidateDescription(string? description)
    // {
    //     if (description?.Length > 1000)
    //         throw new ArgumentException("Salon description cannot be more than 1000", nameof(description));
    // }
}