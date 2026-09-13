using BarberHub.Api.Contracts.Gallery;
using BarberHub.Api.Validators.Messages.Gallery;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Application.Constants;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Gallery;

public class CreateGalleryValidator : AbstractValidator<CreateGalleryRequest>
{
    public CreateGalleryValidator()
    {
        RuleFor(x => x.File)
            .NotNull()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateGalleryValidationMessages.FileProperty));

        RuleFor(x => x.OriginFileName)
            .NotEmpty()
            .WithMessage(SharedValidationMessages.PropertyRequired(CreateGalleryValidationMessages.OriginFileNameProperty))
            .MaximumLength(FileConstants.OriginFileNameMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateGalleryValidationMessages.OriginFileNameProperty));

        RuleFor(x => x.Caption)
            .MaximumLength(GalleryConstants.CaptionMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(CreateGalleryValidationMessages.CaptionProperty));

        RuleFor(x => x.BarberId)
            .GreaterThan(GalleryConstants.BarberIdMinValue)
            .When(x => x.BarberId is not null)
            .WithMessage(CreateGalleryValidationMessages.BarberIdInvalid);

        When(_ => true, () =>
        {
            RuleFor(x => x.File.ContentType)
                .Must(contentType => FileUploadConstants.AllowedContentTypes.Contains(contentType))
                .WithMessage(CreateGalleryValidationMessages.ContentTypeUnsupported);

            RuleFor(x => x.File.Length)
                .GreaterThan(FileConstants.SizeMinLength)
                .WithMessage(CreateGalleryValidationMessages.SizeInvalid)
                .LessThanOrEqualTo(FileUploadConstants.MaxSizeBytes)
                .WithMessage(CreateGalleryValidationMessages.SizeExceeded);
        });
    }
}