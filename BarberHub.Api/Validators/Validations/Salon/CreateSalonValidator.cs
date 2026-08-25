using BarberHub.Api.Contracts.Salon;
using BarberHub.Api.Validators.Messages.Salon;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Salon;

public class CreateSalonValidator : AbstractValidator<CreateSalonRequest>
{
    public CreateSalonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateSalonValidationMessages.NameProperty))
            .MaximumLength(SalonConstants.NameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateSalonValidationMessages.NameProperty));

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateSalonValidationMessages.AddressProperty))
            .MaximumLength(SalonConstants.AddressMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonValidationMessages.AddressProperty));

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateSalonValidationMessages.CityProperty))
            .MaximumLength(SalonConstants.CityMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateSalonValidationMessages.CityProperty));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonValidationMessages.PhoneNumberProperty))
            .Length(SalonConstants.PhoneNumberMaxLength)
            .WithMessage(CreateSalonValidationMessages.PhoneNumberInvalidFormat)
            .Matches($@"^\d{{{SalonConstants.PhoneNumberMaxLength}}}$")
            .WithMessage(CreateSalonValidationMessages.PhoneNumberInvalidFormat);

        RuleFor(x => x.DepositAmountValue)
            .GreaterThan(0)
            .WithMessage(CreateSalonValidationMessages.DepositAmountValueInvalid);

        RuleFor(x => x.DepositAmountCurrency)
            .IsInEnum()
            .WithMessage(CreateSalonValidationMessages.DepositAmountCurrencyInvalid);

        RuleFor(x => x.Description)
            .MaximumLength(SalonConstants.DescriptionMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonValidationMessages.DescriptionProperty));
    }
}