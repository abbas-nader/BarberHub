using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class Customer : BaseEntity
{
    private readonly List<Appointment> _appointments = [];
    private readonly List<WalletTransaction> _walletTransactions = [];

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string MobileNumber { get; private set; } = null!;
    public bool IsMobileVerified { get; private set; }
    public string UserName { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<WalletTransaction> WalletTransactions => _walletTransactions.AsReadOnly();

    private Customer()
    {
    }

    public Customer(string firstName, string lastName, string mobileNumber, string userName,
        string passwordHash, long creationBy)
    {
        ValidateName(firstName, lastName);
        ValidateMobileNumber(mobileNumber);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        MobileNumber = mobileNumber;
        IsMobileVerified = true;
        UserName = userName;
        PasswordHash = passwordHash;
        Creation(creationBy);
    }

    public void UpdateProfile(string firstName, string lastName, string userName, string passwordHash, long modifiedBy)
    {
        ValidateName(firstName, lastName);
        ValidateUserName(userName);
        ValidatePasswordHash(passwordHash);
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        PasswordHash = passwordHash;
        Modified(modifiedBy);
    }

    public void RequestMobileNumberChange(string newMobileNumber, long modifiedBy)
    {
        ValidateMobileNumber(newMobileNumber);
        if (newMobileNumber == MobileNumber) return;
        MobileNumber = newMobileNumber;
        IsMobileVerified = false;
        Modified(modifiedBy);
    }

    public void ConfirmMobileNumberChange(long modifiedBy)
    {
        IsMobileVerified = true;
        Modified(modifiedBy);
    }

    private static void ValidateName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new RequiredFieldException(nameof(firstName));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new RequiredFieldException(nameof(lastName));
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

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }
}