using BarberHub.Api.Contracts.Otp;
using BarberHub.Api.Validators.Messages.Otp;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Otp;

public class VerifyOtpValidator : AbstractValidator<VerifyOtpRequest>
{
    public VerifyOtpValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(VerifyOtpValidationMessages.CodeProperty))
            .Matches(VerifyOtpValidationMessages.CodeValidRegex)
            .WithMessage(VerifyOtpValidationMessages.CodeInvalidFormat);
    }
}