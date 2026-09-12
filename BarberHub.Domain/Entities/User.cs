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

    public new void Update(string firstName, string lastName, string userName, long modifiedBy)
    {
        base.Update(firstName, lastName, userName, modifiedBy);
    }

    public void RequestMobileNumberChange(string newMobileNumber, long modifiedBy)
    {
        ValidateMobileNumber(newMobileNumber);
        if (newMobileNumber == MobileNumber) return;
        MobileNumber = newMobileNumber;
       ConfirmMobileNumberChange(modifiedBy);
        Modified(modifiedBy);
    }

    private void ConfirmMobileNumberChange(long modifiedBy)
    {
        IsMobileVerified = true;
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