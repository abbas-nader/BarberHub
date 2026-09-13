using BarberHub.Api.Contracts.Gallery;
using BarberHub.Api.Validators.Messages.Gallery;
using BarberHub.Api.Validators.Messages.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Gallery;

public class UpdateGalleryValidator : AbstractValidator<UpdateGalleyCaptionRequest>
{
    public UpdateGalleryValidator()
    {
        RuleFor(x => x.Caption)
            .MaximumLength(GalleryConstants.CaptionMaxLength)
            .WithMessage(SharedValidationMessages.PropertyMaxLength(UpdateGalleryValidationMessages.CaptionProperty));
    }
}