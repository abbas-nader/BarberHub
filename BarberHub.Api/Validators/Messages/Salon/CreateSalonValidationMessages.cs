namespace BarberHub.Api.Validators.Messages.Salon;

public static class CreateSalonValidationMessages
{
    public const string NameProperty = "Name";
    public const string AddressProperty = "Address";
    public const string CityProperty = "City";

    public const string PhoneNumberProperty = "PhoneNumber";
    public const string PhoneNumberInvalidFormat = "Phone number format is invalid.";

    public const string DepositAmountValueInvalid = "Deposit amount must be greater than zero.";

    public const string DepositAmountCurrencyInvalid = "Deposit amount currency is invalid.";

    public const string DescriptionProperty = "Description";
}