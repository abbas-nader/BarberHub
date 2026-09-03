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
                SharedValidationMessages.PropertyRequired(CreateBarberValidationMessages.FirstNameProperty))
            .MaximumLength(BarberConstants.FirstNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateBarberValidationMessages.FirstNameProperty));
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(BarberConstants.LastNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateBarberValidationMessages.LastNameProperty));
        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateBarberValidationMessages.MobileNumberProperty))
            .Matches(@"^09\d{9}$")
            .WithMessage(CreateBarberValidationMessages.MobileNumberInvalidFormat);
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateBarberValidationMessages.UsernameProperty))
            .MaximumLength(BarberConstants.UserNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateBarberValidationMessages.UsernameProperty))
            .Matches(@"^\S+$").WithMessage(CreateBarberValidationMessages.UsernameInvalidFormat);
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(CreateBarberValidationMessages.PasswordProperty)
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                SharedValidationMessages.PropertyMinLength(CreateBarberValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateBarberValidationMessages.PasswordProperty));
        RuleFor(x => x.Description)
            .MaximumLength(BarberConstants.DescriptionMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateBarberValidationMessages.DescriptionProperty));
    }
}