using BarberHub.Api.Contracts.Auth;
using BarberHub.Api.Validators.Messages.Auth;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Auth;

public class RegisterValidator : AbstractValidator<RegisterRequset>
{
    public RegisterValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(RegisterValidationMessages.FirstNameProperty))
            .MaximumLength(UserConstants.FirstNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(RegisterValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(RegisterValidationMessages.LastNameProperty))
            .MaximumLength(UserConstants.LastNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(RegisterValidationMessages.LastNameProperty));

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(RegisterValidationMessages.UsernameProperty))
            .MaximumLength(UserConstants.UsernameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(RegisterValidationMessages.UsernameProperty))
            .Matches(RegisterValidationMessages.UserNameRegex)
            .WithMessage(RegisterValidationMessages.UsernameInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(RegisterValidationMessages.PasswordProperty))
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(SharedValidationMessages.PropertyMinLength(RegisterValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(RegisterValidationMessages.PasswordProperty));

        RuleFor(x => x.MobileNumber)
            .Matches(RegisterValidationMessages.MobileNumberRegex)
            .When(_ => true)
            .WithMessage(RegisterValidationMessages.MobileNumberInvalidFormat);
    }
}