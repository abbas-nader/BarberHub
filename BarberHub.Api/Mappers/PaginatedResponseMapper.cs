using BarberHub.Api.Contracts.Shared;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Api.Mappers;

public static class PaginatedResponseMapper
{
    public static PaginatedResponse<TResponse> ToResponse<TSource, TResponse>(
        this PaginatedResult<TSource> result,
        Func<TSource, TResponse> map)
        => new(
            result.Items.Select(map).ToList(),
            result.PageNumber,
            result.PageSize,
            result.TotalCount,
            result.TotalPages
        );
}