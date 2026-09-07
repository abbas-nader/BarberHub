using BarberHub.Api.Contracts.Auth;
using BarberHub.Api.Validators.Messages.Auth;
using BarberHub.Api.Validators.Messages.Shared;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Auth;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(LoginValidationMessages.UsernameProperty));

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(LoginValidationMessages.PasswordProperty));

        RuleFor(x => x.Role)
            .IsInEnum()
            .WithMessage(LoginValidationMessages.RoleInvalid);
    }
}