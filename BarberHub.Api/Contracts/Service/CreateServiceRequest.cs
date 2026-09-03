namespace BarberHub.Api.Contracts.Service;

public record CreateServiceRequest(
    string Name,
    string? Description,
    TimeSpan Duration
);