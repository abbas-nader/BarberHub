using BarberHub.Api.Contracts.Salon;
using BarberHub.Api.Validators.Messages.Salon;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Salon;

public class UpdateSalonDepositAmountValidator: AbstractValidator<UpdateSalonDepositAmountRequest>
{
    public UpdateSalonDepositAmountValidator()
    {
        RuleFor(x => x.DepositAmountValue)
            .GreaterThan(UpdateSalonDepositAmountValidationMessages.ValueMinValidValue)
            .WithMessage(UpdateSalonDepositAmountValidationMessages.DepositAmountValueInvalid);

        RuleFor(x => x.DepositAmountCurrency)
            .IsInEnum()
            .WithMessage(UpdateSalonDepositAmountValidationMessages.DepositAmountCurrencyInvalid);
    }
}