using BarberHub.Domain.Constants;

namespace BarberHub.Api.Validators.Messages.Salon;

public static class UpdateSalonValidationMessages
{
    public const string NameProperty = "Name";
    public const string AddressProperty = "Address";
    public const string CityProperty = "City";

    public const string PhoneNumberProperty = "PhoneNumber";
    public const string PhoneNumberInvalidFormat = "Phone number format is invalid.";
    public static readonly string PhonNumberRegex = $@"^[0-9]{{{SalonConstants.PhoneNumberMaxLength}}}$";

    
    public const string DescriptionProperty = "Description";
}