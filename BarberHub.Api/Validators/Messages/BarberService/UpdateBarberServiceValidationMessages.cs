namespace BarberHub.Api.Validators.Messages.BarberService;

public static class UpdateBarberServiceValidationMessages
{
    public const string PriceValueProperty = "PriceValue";
    public const string PriceValueInvalid = "Price must be greater than zero.";

    public const string CurrencyProperty = "PriceCurrency";
    public const string CurrencyInvalid = "Currency is invalid.";

    public const string DurationProperty = "Duration";
    public const string DurationInvalid = "Duration must be greater than zero.";
}