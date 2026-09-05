namespace BarberHub.Api.Constants.Authentication;

public static class AuthUriConstants
{
    private const string ControllerName = "auth";
    public const string LoginSalonAdmin = $"{ControllerName}/login/salon-admin";
    public const string LoginBarber = $"{ControllerName}/login/barber";
    public const string LoginCustomer = $"{ControllerName}/login/customer";
    public const string LoginPlatformAdmin = $"{ControllerName}/login/platform-admin";
    public const string Refresh = $"{ControllerName}/refresh";
    public const string Revoke = $"{ControllerName}/revoke";
}