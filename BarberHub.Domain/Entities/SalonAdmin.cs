using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class SalonAdmin : BaseUser
{
    public string MobileNumber { get; private set; } = null!;
    public bool IsMobileVerified { get; private set; }

    public long SalonId { get; private set; }
    public Salon Salon { get; private set; } = null!;

    private SalonAdmin()
    {
    }

    public SalonAdmin(string firstName, string lastName, string userName, string passwordHash, string mobileNumber,
        long salonId, long creationBy)
        : base(firstName, lastName, userName, passwordHash)
    {
        ValidateMobileNumber(mobileNumber);
        MobileNumber = mobileNumber;
        IsMobileVerified = true;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void Update(string firstName, string lastName, string userName, string passwordHash, string mobileNumber,
        long modifiedBy)
    {
        Update(firstName, lastName, userName, modifiedBy);
        ValidateMobileNumber(mobileNumber);
        MobileNumber = mobileNumber;
    }

    private static void ValidateMobileNumber(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new RequiredFieldException(nameof(mobileNumber));
    }
}