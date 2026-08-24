using System.ComponentModel;

namespace BarberHub.Domain.Constants;

public static class SalonConstants
{
    public const int DescriptionMaxLength = 1000;
    public const int NameMaxLength = 100;
    public const int AddressMaxLength = 500;
    public const int CityMaxLength = 50;
    public const int PhoneNumberMaxLength = 11;
    public const string DepositAmountValueColumnName = "DepositAmountValue";
    public const string DepositAmountColumnType = "numeric(18,2)";
    public const string DepositAmountCurrencyCodeColumnName = "DepositAmountCurrency";
    
    
}