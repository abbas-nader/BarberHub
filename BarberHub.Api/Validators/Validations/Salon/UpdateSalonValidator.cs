using BarberHub.Api.Contracts.Salon;
using BarberHub.Api.Validators.Messages.Salon;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Salon;

public class UpdateSalonValidator : AbstractValidator<UpdateSalonRequest>
{
    public UpdateSalonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateSalonValidationMessages.NameProperty))
            .MaximumLength(SalonConstants.NameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateSalonValidationMessages.NameProperty));

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateSalonValidationMessages.AddressProperty))
            .MaximumLength(SalonConstants.AddressMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonValidationMessages.AddressProperty));

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateSalonValidationMessages.CityProperty))
            .MaximumLength(SalonConstants.CityMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateSalonValidationMessages.CityProperty));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(
               SharedValidationMessages.PropertyRequired(UpdateSalonValidationMessages.PhoneNumberProperty))
            .Matches($@"^\d{UpdateSalonValidationMessages.PhoneNumberMaxLength}$")
            .WithMessage(UpdateSalonValidationMessages.PhoneNumberInvalidFormat);

        RuleFor(x => x.Description)
            .MaximumLength(SalonConstants.DescriptionMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonValidationMessages.DescriptionProperty));
    }
}