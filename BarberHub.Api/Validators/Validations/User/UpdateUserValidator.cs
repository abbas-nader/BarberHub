using BarberHub.Api.Contracts.User;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Api.Validators.Messages.User;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.User;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateUserValidationMessages.FirstNameProperty))
            .MaximumLength(UserConstants.FirstNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateUserValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateUserValidationMessages.LastNameProperty))
            .MaximumLength(UserConstants.LastNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateUserValidationMessages.LastNameProperty));

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateUserValidationMessages.UsernameProperty))
            .MaximumLength(UserConstants.UsernameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateUserValidationMessages.UsernameProperty))
            .Matches(UpdateUserValidationMessages.UserNameRegex)
            .WithMessage(UpdateUserValidationMessages.UsernameInvalidFormat);
    }
}