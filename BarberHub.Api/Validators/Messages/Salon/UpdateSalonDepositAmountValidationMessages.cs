namespace BarberHub.Api.Validators.Messages.Salon;

public static class UpdateSalonDepositAmountValidationMessages
{
    public const string DepositAmountValueProperty = "DepositAmountValue";
    public const string DepositAmountValueInvalid = "Deposit amount must be greater than zero.";
    public const int ValueMinValidValue = 0;
    
    public const string DepositAmountCurrencyProperty = "DepositAmountCurrency";
    public const string DepositAmountCurrencyInvalid = "Deposit amount currency is invalid.";
    
}