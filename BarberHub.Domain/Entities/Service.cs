using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Service : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public long SalonId { get; private set; }

    private Service()
    {
    }

    public Service(string name, string? description, long salonId, long creationBy)
    {
        ValidateName(name);
        ValidateDescription(description);
        Name = name;
        Description = description;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void UpdateService(string name, string? description, long modifiedBy)
    {
        ValidateName(name);
        ValidateDescription(description);
        Name = name;
        Description = description;
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
}