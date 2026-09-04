using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Validators.Messages.Barber;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Barber;

public class UpdateBarberValidator : AbstractValidator<UpdateBarberRequest>
{
    public UpdateBarberValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateBarberValidationMessages.FirstNameProperty))
            .MaximumLength(BarberConstants.FirstNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateBarberValidationMessages.FirstNameProperty));
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(BarberConstants.LastNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateBarberValidationMessages.LastNameProperty));
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateBarberValidationMessages.MobileNumberProperty))
            .Matches(@"^09\d{9}$")
            .WithMessage(UpdateBarberValidationMessages.MobileNumberInvalidFormat);
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateBarberValidationMessages.UsernameProperty))
            .MaximumLength(BarberConstants.UserNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateBarberValidationMessages.UsernameProperty))
            .Matches(@"^\S+$")
            .WithMessage(UpdateBarberValidationMessages.UsernameInvalidFormat);
        RuleFor(x => x.Password)
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                SharedValidationMessages.PropertyMinLength(UpdateBarberValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateBarberValidationMessages.PasswordProperty))
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
        RuleFor(x => x.Description)
            .MaximumLength(BarberConstants.DescriptionMaxLength).WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateBarberValidationMessages.DescriptionProperty));
    }
}