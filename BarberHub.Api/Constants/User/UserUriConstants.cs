namespace BarberHub.Api.Constants.User;

public static class UserUriConstants
{
    private const string ControllerName = "user";
    public const string GetMe = $"{ControllerName}/me";
    public const string UpdateMe = $"{ControllerName}/update";
    public const string ChangePassword = $"{ControllerName}/change-password";
    public const string Delete = $"{ControllerName}/delete";
}