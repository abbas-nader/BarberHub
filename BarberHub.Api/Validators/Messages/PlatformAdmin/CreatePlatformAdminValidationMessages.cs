namespace BarberHub.Api.Validators.Messages.PlatformAdmin;

public static class CreatePlatformAdminValidationMessages
{
    public const string FirstNameProperty = "FirstName";

    public const string LastNameProperty = "LastName";

    public const string UserNameProperty = "UserName";
    public const string UserNameInvalidFormat = "Username format is invalid.";
    public const string UserNameRegex = @"^\S+$";

    public const string PasswordProperty = "Password";
}