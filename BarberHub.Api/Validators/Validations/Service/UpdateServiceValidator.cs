using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Contracts.Service;
using BarberHub.Api.Validators.Messages.Barber;
using BarberHub.Api.Validators.Messages.Service;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Service;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceRequest>
{
    public UpdateServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateServiceValidationMessages.NameProperty))
            .MaximumLength(ServiceConstants.NameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateServiceValidationMessages.NameProperty));

        RuleFor(x => x.Description)
            .MaximumLength(ServiceConstants.DescriptionMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateServiceValidationMessages.DescriptionProperty));
    }
}