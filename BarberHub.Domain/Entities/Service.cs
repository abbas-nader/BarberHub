using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public TimeSpan Duration { get; private set; }

    public long SalonId { get; private set; }

    private Service()
    {
    }

    public Service(string name, string? description, TimeSpan duration, long salonId, long creationBy)
    {
        ValidateName(name);
        ValidateDescription(description);
        ValidateDuration(duration);
        Name = name;
        Description = description;
        Duration = duration;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void UpdateService(string name, string? description, TimeSpan duration, long modifiedBy)
    {
        ValidateName(name);
        ValidateDescription(description);
        ValidateDuration(duration);
        Name = name;
        Description = description;
        Duration = duration;
        Modified(modifiedBy);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new RequiredFieldException(nameof(name));
        }
    }

    private static void ValidateDescription(string? description)
    {
        if (description is { Length: > ServiceConstants.DescriptionMaxLength })
            throw new InvalidServiceDescriptionException();
    }

    private static void ValidateDuration(TimeSpan duration)
    {
        if (duration.Ticks < ServiceConstants.DurationMinValue)
            throw new InvalidServiceDurationException();
    }
}