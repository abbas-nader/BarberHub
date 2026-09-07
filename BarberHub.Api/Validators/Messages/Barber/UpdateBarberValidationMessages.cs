namespace BarberHub.Api.Validators.Messages.Barber;

public static class UpdateBarberValidationMessages
{
    public const string IdProperty = "Id";
    
    public const string FirstNameProperty = "FirstName";
    
    public const string LastNameProperty = "LastName";
    
    public const string MobileNumberProperty = "MobileNumber";
    public const string MobileNumberInvalidFormat = "Mobile number format is invalid.";
    public const string MobileNumberRegex = @"^09\d{9}$";
    
    public const string UsernameProperty = "Username";
    public const string UsernameInvalidFormat = "Username format is invalid.";
    public const string UserNameRegex = @"^\S+$";
    
    public const string DescriptionProperty = "Description";
    
    public const string SalonIdInvalid = "Salon id must be a positive number.";
}