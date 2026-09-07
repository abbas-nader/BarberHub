namespace BarberHub.Api.Validators.Messages.Shared;

public static class ChangePasswordValidationMessages
{
    public const string OldPasswordProperty = "OldPassword";
    public const string NewPasswordProperty = "NewPassword";
    public const string NewPasswordSameAsOld = "New password must be different from the old password.";
}