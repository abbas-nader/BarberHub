using BarberHub.Api.Contracts.PlatformAdmin;
using BarberHub.Api.Validators.Messages.PlatformAdmin;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.PlatformAdmin;

public class UpdatePlatformAdminValidator: AbstractValidator<UpdatePlatformAdminRequest>
{
    public UpdatePlatformAdminValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdatePlatformAdminValidationMessages.FirstNameProperty))
            .MaximumLength(UserConstants.FirstNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdatePlatformAdminValidationMessages.FirstNameProperty));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdatePlatformAdminValidationMessages.LastNameProperty))
            .MaximumLength(UserConstants.LastNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdatePlatformAdminValidationMessages.LastNameProperty));

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UpdatePlatformAdminValidationMessages.UserNameProperty))
            .MaximumLength(UserConstants.UsernameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdatePlatformAdminValidationMessages.UserNameProperty))
            .Matches(UpdatePlatformAdminValidationMessages.UserNameRegex)
            .WithMessage(UpdatePlatformAdminValidationMessages.UserNameInvalidFormat);
    }
}