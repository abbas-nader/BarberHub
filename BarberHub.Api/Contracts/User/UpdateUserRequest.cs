namespace BarberHub.Api.Contracts.User;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string UserName
);