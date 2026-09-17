namespace BarberHub.Api.Contracts.Shared;

public record PaginationRequest(
    int PageNumber,
    int PageSize
);