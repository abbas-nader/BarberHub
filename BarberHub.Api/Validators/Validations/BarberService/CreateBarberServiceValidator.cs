using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Contracts.BarberService;
using BarberHub.Api.Validators.Messages.BarberService;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.BarberService;

public class CreateBarberServiceValidator : AbstractValidator<CreateBarberServiceRequest>
{
    public CreateBarberServiceValidator()
    {
        RuleFor(x => x.BarberId)
            .GreaterThan(0)
            .WithMessage(CreateBarberServiceValidationMessages.BarberIdInvalid);

        RuleFor(x => x.ServiceId)
            .GreaterThan(0)
            .WithMessage(CreateBarberServiceValidationMessages.ServiceIdInvalid);

        RuleFor(x => x.PriceValue)
            .GreaterThan(0)
            .WithMessage(CreateBarberServiceValidationMessages.PriceValueInvalid);

        RuleFor(x => x.PriceCurrency)
            .IsInEnum()
            .WithMessage(CreateBarberServiceValidationMessages.CurrencyInvalid);

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage(CreateBarberServiceValidationMessages.DurationInvalid);
    }
}