using BarberHub.Api.Contracts.EndUser;
using BarberHub.Api.Validators.Messages.EndUser;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.EndUser;

public class UpdateEndUserValidator : AbstractValidator<UpdateEndUserRequest>
{
    public UpdateEndUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateEndUserValidationMessages.FirstNameProperty))
            .MaximumLength(UserConstants.FirstNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateEndUserValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateEndUserValidationMessages.LastNameProperty))
            .MaximumLength(UserConstants.LastNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateEndUserValidationMessages.LastNameProperty));

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdateEndUserValidationMessages.UsernameProperty))
            .MaximumLength(UserConstants.UsernameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateEndUserValidationMessages.UsernameProperty))
            .Matches(UpdateEndUserValidationMessages.UserNameRegex)
            .WithMessage(UpdateEndUserValidationMessages.UsernameInvalidFormat);

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateEndUserValidationMessages.MobileNumberProperty))
            .Matches(UpdateEndUserValidationMessages.MobileNumberRegex)
            .WithMessage(UpdateEndUserValidationMessages.MobileNumberInvalidFormat);
    }
}