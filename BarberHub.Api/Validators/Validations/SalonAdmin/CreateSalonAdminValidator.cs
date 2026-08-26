using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Api.Validators.Messages.SalonAdmin;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.SalonAdmin;

public class CreateSalonAdminValidator : AbstractValidator<CreateSalonAdminRequest>
{
    public CreateSalonAdminValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonAdminValidationMessages.FirstNameProperty))
            .MaximumLength(SalonAdminConstants.FirstNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonAdminValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonAdminValidationMessages.LastNameProperty))
            .MaximumLength(SalonAdminConstants.LastNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonAdminValidationMessages.LastNameProperty));

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonAdminValidationMessages.MobileNumberProperty))
            .Matches(@"^09\d{9}$")
            .WithMessage(CreateSalonAdminValidationMessages.MobileNumberInvalidFormat);

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonAdminValidationMessages.UsernameProperty))
            .MaximumLength(SalonAdminConstants.UsernameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonAdminValidationMessages.UsernameProperty))
            .Matches(@"^\S+$")
            .WithMessage(CreateSalonAdminValidationMessages.UsernameInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(CreateSalonAdminValidationMessages.PasswordProperty))
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                SharedValidationMessages.PropertyMinLength(CreateSalonAdminValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(CreateSalonAdminValidationMessages.PasswordProperty));

        RuleFor(x => x.SalonId)
            .GreaterThan(SalonAdminConstants.SalonMinValidValue)
            .WithMessage(CreateSalonAdminValidationMessages.SalonIdInvalid);
    }
}