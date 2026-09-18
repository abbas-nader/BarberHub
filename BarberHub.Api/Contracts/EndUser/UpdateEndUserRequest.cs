namespace BarberHub.Api.Contracts.EndUser;

public record UpdateEndUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string MobileNumber
);