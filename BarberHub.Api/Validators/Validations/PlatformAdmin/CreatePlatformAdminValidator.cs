using BarberHub.Api.Contracts.PlatformAdmin;
using BarberHub.Api.Validators.Messages.PlatformAdmin;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.PlatformAdmin;

public class CreatePlatformAdminValidator: AbstractValidator<CreatePlatformAdminRequest>
{
    public CreatePlatformAdminValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreatePlatformAdminValidationMessages.FirstNameProperty))
            .MaximumLength(UserConstants.FirstNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreatePlatformAdminValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreatePlatformAdminValidationMessages.LastNameProperty))
            .MaximumLength(UserConstants.LastNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreatePlatformAdminValidationMessages.LastNameProperty));

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreatePlatformAdminValidationMessages.UserNameProperty))
            .MaximumLength(UserConstants.UsernameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreatePlatformAdminValidationMessages.UserNameProperty))
            .Matches(CreatePlatformAdminValidationMessages.UserNameRegex)
            .WithMessage(CreatePlatformAdminValidationMessages.UserNameInvalidFormat);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreatePlatformAdminValidationMessages.PasswordProperty))
            .MinimumLength(PasswordValidationMessages.MinLength)
            .WithMessage(SharedValidationMessages.PropertyMinLength(CreatePlatformAdminValidationMessages.PasswordProperty))
            .MaximumLength(PasswordValidationMessages.MaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreatePlatformAdminValidationMessages.PasswordProperty));
    }
}