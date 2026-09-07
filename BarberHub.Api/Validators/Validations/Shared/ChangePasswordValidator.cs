using BarberHub.Api.Contracts.Shared;
using BarberHub.Api.Validators.Messages.Shared;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Shared;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(ChangePasswordValidationMessages.OldPasswordProperty));

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage(
                SharedValidationMessages.PropertyRequired(ChangePasswordValidationMessages.NewPasswordProperty))
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(
                SharedValidationMessages.PropertyMinLength(ChangePasswordValidationMessages.NewPasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(
                SharedValidationMessages.PropertyMaxLength(ChangePasswordValidationMessages.NewPasswordProperty))
            .NotEqual(x => x.OldPassword)
            .WithMessage(ChangePasswordValidationMessages.NewPasswordSameAsOld);
    }
}