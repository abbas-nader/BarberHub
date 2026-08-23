using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Validators.Messages.Barber;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Barber;

public class CreateBarberValidator : AbstractValidator<CreateBarberRequest>
{
    public CreateBarberValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                CreateBarberValidationMessages.PropertyRequired(
                    nameof(CreateBarberValidationMessages.FirstNameProperty)))
            .MaximumLength(BarberConstants.FirstNameMaxLength)
            .WithMessage(
                CreateBarberValidationMessages.PropertyMaxLength(
                    nameof(CreateBarberValidationMessages.FirstNameProperty)));
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(BarberConstants.LastNameMaxLength)
            .WithMessage(
                CreateBarberValidationMessages.PropertyMaxLength(
                    nameof(CreateBarberValidationMessages.LastNameProperty)));
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                CreateBarberValidationMessages.PropertyRequired(nameof(CreateBarberValidationMessages
                    .MobileNumberProperty)))
            .Matches(@"^09\d{9}$")
            .WithMessage(CreateBarberValidationMessages.MobileNumberInvalidFormat);
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                CreateBarberValidationMessages.PropertyRequired(nameof(CreateBarberValidationMessages
                    .UsernameProperty)))
            .MaximumLength(BarberConstants.UserNameMaxLength)
            .WithMessage(CreateBarberValidationMessages.PropertyMaxLength(nameof(CreateBarberValidationMessages
                .UsernameProperty)))
            .Matches(@"^\S+$")
            .WithMessage(CreateBarberValidationMessages.UsernameInvalidFormat);
        RuleFor(x => x.Password)
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                CreateBarberValidationMessages.PropertyMinLength(
                    nameof(CreateBarberValidationMessages.PasswordProperty)))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                CreateBarberValidationMessages.PropertyMaxLength(
                    nameof(CreateBarberValidationMessages.PasswordProperty)))
            .When(x => !string.IsNullOrWhiteSpace(x.Password));
        RuleFor(x => x.Description)
            .MaximumLength(BarberConstants.DescriptionMaxLength)
            .WithMessage(CreateBarberValidationMessages.PropertyMaxLength(nameof(CreateBarberValidationMessages
                .DescriptionProperty)));
        RuleFor(x => x.SalonId)
            .GreaterThan(BarberConstants.SalonMinValidValue)
            .WithMessage(CreateBarberValidationMessages.SalonIdInvalid);
    }
}