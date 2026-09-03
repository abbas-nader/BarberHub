namespace BarberHub.Api.Contracts.Service;

public record UpdateServiceRequest(
    long Id,
    string Name,
    string? Description,
    TimeSpan Duration
);