using BarberHub.Api.Contracts.SalonAdmin;
using BarberHub.Api.Validators.Messages.SalonAdmin;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.SalonAdmin;

public class UpdateSalonAdminValidator : AbstractValidator<UpdateSalonAdminRequest>
{
    public UpdateSalonAdminValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(SalonAdminConstants.SalonMinValidValue)
            .WithMessage(UpdateSalonAdminValidationMessages.IdInvalid);
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateSalonAdminValidationMessages.FirstNameProperty))
            .MaximumLength(SalonAdminConstants.FirstNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonAdminValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateSalonAdminValidationMessages.LastNameProperty))
            .MaximumLength(SalonAdminConstants.LastNameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonAdminValidationMessages.LastNameProperty));

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateSalonAdminValidationMessages.UsernameProperty))
            .MaximumLength(SalonAdminConstants.UsernameMaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonAdminValidationMessages.UsernameProperty))
            .Matches(@"^\S+$")
            .WithMessage(UpdateSalonAdminValidationMessages.UsernameInvalidFormat);

        RuleFor(x => x.Password)
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                SharedValidationMessages.PropertyMinLength(UpdateSalonAdminValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(UpdateSalonAdminValidationMessages.PasswordProperty))
            .When(x => !string.IsNullOrWhiteSpace(x.Password));

        RuleFor(x => x.MobileNumber)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(UpdateSalonAdminValidationMessages.MobileNumberProperty))
            .Matches(@"^09\d{9}$")
            .WithMessage(UpdateSalonAdminValidationMessages.MobileNumberInvalidFormat);
    }
}