namespace BarberHub.Api.Contracts.Service;

public record UpdateServiceRequest(
    string Name,
    string? Description
);