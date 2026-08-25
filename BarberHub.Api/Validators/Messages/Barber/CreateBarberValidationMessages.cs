namespace BarberHub.Api.Validators.Messages.Barber;

public static class CreateBarberValidationMessages
{
    public const string FirstNameProperty = "FirstName";
    
    public const string LastNameProperty = "LastName";
    
    public const string MobileNumberProperty = "MobileNumber";
    public const string MobileNumberInvalidFormat = "Mobile number format is invalid.";
    
    public const string UsernameProperty = "Username";
    public const string UsernameInvalidFormat = "Username format is invalid.";
    
    public const string PasswordProperty = "Password";
    
    public const string DescriptionProperty = "Description";
    
    public const string SalonIdInvalid = "Salon id must be a positive number.";
}