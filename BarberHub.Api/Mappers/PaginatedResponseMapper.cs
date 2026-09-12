using BarberHub.Api.Contracts.Shared;
using BarberHub.Application.DTOs.Shared;

namespace BarberHub.Api.Mappers;

public static class PaginatedResponseMapper
{
    public static PaginatedResponse<TResponse> ToResponse<TSource, TResponse>(
        this PagedResult<TSource> result,
        Func<TSource, TResponse> map)
        => new(
            result.Items.Select(map).ToList(),
            result.PageNumber,
            result.PageSize,
            result.TotalCount,
            result.TotalPages
        );
}