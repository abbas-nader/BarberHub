namespace BarberHub.Api.Validators.Messages.User;

public static class UpdateUserValidationMessages
{
    public const string FirstNameProperty = "FirstName";

    public const string LastNameProperty = "LastName";

    public const string UsernameProperty = "UserName";
    public const string UsernameInvalidFormat = "Username format is invalid.";
    public const string UserNameRegex = @"^\S+$";
}