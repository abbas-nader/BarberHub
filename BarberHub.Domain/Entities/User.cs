using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.Enums;

namespace BarberHub.Domain.Entities;

public class User : BaseUser
{
    private readonly List<Appointment> _appointments = [];
    private readonly List<WalletTransaction> _walletTransactions = [];

    public string MobileNumber { get; private set; } = null!;
    public bool IsMobileVerified { get; private set; }

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<WalletTransaction> WalletTransactions => _walletTransactions.AsReadOnly();

    private User()
    {
    }

    public User(string firstName, string lastName, string mobileNumber, string userName,
        string passwordHash, long creationBy)
        : base(firstName, lastName, userName, passwordHash)
    {
        ValidateMobileNumber(mobileNumber);
        MobileNumber = mobileNumber;
        IsMobileVerified = true;
        Creation(creationBy);
    }

    public void UpdateProfile(string firstName, string lastName, string userName, string passwordHash, long modifiedBy)
    {
        Update(firstName, lastName, userName, modifiedBy);
    }

    public void RequestMobileNumberChange(string newMobileNumber, long modifiedBy)
    {
        ValidateMobileNumber(newMobileNumber);
        if (newMobileNumber == MobileNumber) return;
        MobileNumber = newMobileNumber;
        IsMobileVerified = true;
        Modified(modifiedBy);
    }

    public void ConfirmMobileNumberChange(long modifiedBy)
    {
        IsMobileVerified = true;
        Modified(modifiedBy);
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }
}