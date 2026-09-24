namespace BarberHub.Api.Validators.Messages.EndUser;

public static class UpdateEndUserValidationMessages
{
    public const string FirstNameProperty = "FirstName";
    public const string LastNameProperty = "LastName";

    public const string UsernameProperty = "UserName";
    public const string UsernameInvalidFormat = "Username format is invalid.";
    public const string UserNameRegex = @"^\S+$";

    public const string MobileNumberProperty = "MobileNumber";
    public const string MobileNumberInvalidFormat = "Mobile number format is invalid.";
    public const string MobileNumberRegex = @"^09[0-9]{9}$";
}