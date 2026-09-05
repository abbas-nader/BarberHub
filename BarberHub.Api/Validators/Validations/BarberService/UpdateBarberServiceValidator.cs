using BarberHub.Api.Validators.Messages.BarberService;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.BarberService;

public class UpdateBarberServiceValidator :  AbstractValidator<Domain.Entities.BarberService>
{
    public UpdateBarberServiceValidator()
    {
        RuleFor(x => x.Price.Value)
            .GreaterThan(0)
            .WithMessage(UpdateBarberServiceValidationMessages.PriceValueInvalid);

        RuleFor(x => x.Price.Currency)
            .IsInEnum()
            .WithMessage(UpdateBarberServiceValidationMessages.CurrencyInvalid);

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage(UpdateBarberServiceValidationMessages.DurationInvalid);
    }
}