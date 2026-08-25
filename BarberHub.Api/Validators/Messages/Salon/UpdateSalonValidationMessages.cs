namespace BarberHub.Api.Validators.Messages.Salon;

public class UpdateSalonValidationMessages
{
    public const string NameProperty = "Name";
    public const string AddressProperty = "Address";
    public const string CityProperty = "City";

    public const string PhoneNumberProperty = "PhoneNumber";
    public const string PhoneNumberInvalidFormat = "Phone number format is invalid.";
    public const int PhoneNumberMaxLength = 11;

    public const string DescriptionProperty = "Description";
}