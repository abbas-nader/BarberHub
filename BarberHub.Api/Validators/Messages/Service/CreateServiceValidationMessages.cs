namespace BarberHub.Api.Validators.Messages.Service;

public static class CreateServiceValidationMessages
{
    public const string NameProperty = "Name";
    
    public const string DescriptionProperty = "Description";
    
    public const string DurationProperty = "Duration";

    public const string DurationInvalid = "Duration must be greater than zero.";
}