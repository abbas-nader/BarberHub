namespace BarberHub.Api.Contracts.Service;

public record ServiceResponse(
    long Id,
    string Name,
    string? Description
);