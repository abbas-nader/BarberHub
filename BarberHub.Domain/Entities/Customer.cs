using BarberHub.Domain.Exceptions;

namespace BarberHub.Domain.Entities;

public class Customer : BaseEntity
{
    private readonly List<Appointment> _appointments = [];
    private readonly List<WalletTransaction> _walletTransactions = [];
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

    public IReadOnlyCollection<WalletTransaction> WalletTransactions =>
        _walletTransactions.AsReadOnly();

    private Customer()
    {
    }

    private Customer(string firstName, string lastName, string mobileNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
    }

    public static Customer Create(string firstName, string lastName, string mobileNumber, long creationBy)
    {
        ValidateName(firstName, lastName);
        ValidateMobileNumber(mobileNumber);
        var customer = new Customer(firstName, lastName, mobileNumber);
        customer.SetCreationInfo(creationBy);
        return customer;
    }

    public void UpdateProfile(string firstName, string lastName, string mobileNumber, long modifiedBy)
    {
       ValidateName(firstName, lastName);
       ValidateMobileNumber(mobileNumber);
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        MarkAsModified(modifiedBy);
    }

    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidFirstNameException();
        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidLastNameException();
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (mobileNumber.Length != 11 || !mobileNumber.StartsWith("09"))
            throw new InvalidMobileNumberException();
    }
}