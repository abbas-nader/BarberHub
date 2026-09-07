namespace BarberHub.Api.Contracts.Shared;

public record PaginatedResponse<T>(
    IReadOnlyCollection<T> Items,
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages
);