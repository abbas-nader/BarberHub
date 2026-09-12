using BarberHub.Api.Contracts.BarberService;
using BarberHub.Api.Validators.Messages.BarberService;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.BarberService;

public class UpdateBarberServiceValidator :  AbstractValidator<UpdateBarberServiceRequest>
{
    public UpdateBarberServiceValidator()
    {
        RuleFor(x => x.PriceValue)
            .GreaterThan(0)
            .WithMessage(UpdateBarberServiceValidationMessages.PriceValueInvalid);

        RuleFor(x => x.PriceCurrency)
            .IsInEnum()
            .WithMessage(UpdateBarberServiceValidationMessages.CurrencyInvalid);

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage(UpdateBarberServiceValidationMessages.DurationInvalid);
    }
}