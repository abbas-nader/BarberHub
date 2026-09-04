using BarberHub.Api.Contracts.Service;
using BarberHub.Api.Validators.Messages.Service;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Service;

public class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateServiceValidationMessages.NameProperty))
            .MaximumLength(ServiceConstants.NameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateServiceValidationMessages.NameProperty));

        RuleFor(x => x.Description)
            .MaximumLength(ServiceConstants.DescriptionMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateServiceValidationMessages.DescriptionProperty));

        RuleFor(x => x.Duration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage(CreateServiceValidationMessages.DurationInvalid);
    }
}