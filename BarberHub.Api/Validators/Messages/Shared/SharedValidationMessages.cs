namespace BarberHub.Api.Validators.Messages.Shared;

public static class SharedValidationMessages
{
    public static string PropertyRequired(string propertyName) => $"{propertyName} is required.";

    public static string PropertyMaxLength(string propertyName) =>
        $"{propertyName} exceeds the maximum allowed length.";

    public static string PropertyMinLength(string propertyName) =>
        $"{propertyName} is shorter than the minimum required length.";
}