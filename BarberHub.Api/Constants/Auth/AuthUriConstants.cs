namespace BarberHub.Api.Constants.Auth;

public static class AuthUriConstants
{
    private const string ControllerName = "auth";
    public const string Login = $"{ControllerName}/login";
    public const string Refresh = $"{ControllerName}/refresh";
    public const string Revoke = $"{ControllerName}/revoke";
}