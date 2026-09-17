using BarberHub.Api.Contracts.Shared;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.Shared;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(PaginationConstants.MinPageNumber - 1);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(
                PaginationConstants.MinPageSize,
                PaginationConstants.MaxPageSize);
    }
}