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
                UpdateBarberValidationMessages.PropertyRequired(
                    nameof(UpdateBarberValidationMessages.FirstNameProperty)))
            .MaximumLength(BarberConstants.FirstNameMaxLength)
            .WithMessage(
                UpdateBarberValidationMessages.PropertyMaxLength(
                    nameof(UpdateBarberValidationMessages.LastNameProperty)));
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(BarberConstants.LastNameMaxLength)
            .WithMessage(
                UpdateBarberValidationMessages.PropertyMaxLength(
                    nameof(UpdateBarberValidationMessages.LastNameProperty)));
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                UpdateBarberValidationMessages.PropertyRequired(nameof(UpdateBarberValidationMessages
                    .MobileNumberProperty)))
            .Matches(@"^09\d{9}$")
            .WithMessage(UpdateBarberValidationMessages.MobileNumberInvalidFormat);
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                UpdateBarberValidationMessages.PropertyRequired(nameof(UpdateBarberValidationMessages
                    .UsernameProperty)))
            .MaximumLength(BarberConstants.UserNameMaxLength)
            .WithMessage(UpdateBarberValidationMessages.PropertyMaxLength(nameof(UpdateBarberValidationMessages
                .UsernameProperty)))
            .Matches(@"^\S+$")
            .WithMessage(UpdateBarberValidationMessages.UsernameInvalidFormat);
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(UpdateBarberValidationMessages.PropertyRequired(nameof(UpdateBarberValidationMessages
                .PasswordProperty)))
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(UpdateBarberValidationMessages.PropertyMinLength(nameof(UpdateBarberValidationMessages
                .PasswordProperty)))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(UpdateBarberValidationMessages.PropertyMaxLength(nameof(UpdateBarberValidationMessages
                .PasswordProperty)));
        RuleFor(x => x.Description)
            .MaximumLength(BarberConstants.DescriptionMaxLength)
            .WithMessage(UpdateBarberValidationMessages.PropertyMaxLength(nameof(UpdateBarberValidationMessages
                .DescriptionProperty)));
    }
}