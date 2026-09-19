namespace BarberHub.Api.Validators.Messages.PlatformAdmin;

public static class UpdatePlatformAdminValidationMessages
{
    public const string FirstNameProperty = "FirstName";

    public const string LastNameProperty = "LastName";

    public const string UserNameProperty = "UserName";
    public const string UserNameInvalidFormat = "Username format is invalid.";
    public const string UserNameRegex = @"^\S+$";
}