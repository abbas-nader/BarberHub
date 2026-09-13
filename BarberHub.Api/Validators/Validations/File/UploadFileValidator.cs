using BarberHub.Api.Contracts.File;
using BarberHub.Api.Validators.Messages.File;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Application.Constants;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.File;

public class UploadFileValidator : AbstractValidator<UploadFileRequest>
{
    public UploadFileValidator()
    {
        RuleFor(x => x.FileStream)
            .NotNull()
            .WithMessage(SharedValidationMessages.PropertyRequired(UploadFileValidationMessages.FileStreamProperty));

        RuleFor(x => x.OriginFileName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UploadFileValidationMessages.OriginFileNameProperty))
            .MaximumLength(FileConstants.OriginFileNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UploadFileValidationMessages.OriginFileNameProperty));

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(UploadFileValidationMessages.ContentTypeProperty))
            .Must(contentType => FileUploadConstants.AllowedContentTypes.Contains(contentType))
            .WithMessage(UploadFileValidationMessages.ContentTypeUnsupported);

        RuleFor(x => x.Size)
            .GreaterThan(FileConstants.SizeMinLength)
            .WithMessage(UploadFileValidationMessages.SizeInvalid)
            .LessThanOrEqualTo(FileUploadConstants.MaxSizeBytes)
            .WithMessage(UploadFileValidationMessages.SizeExceeded);
    }
}