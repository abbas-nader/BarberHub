using BarberHub.Application.Security.Hash;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Infrastructure.Security.Hash;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return string.IsNullOrWhiteSpace(password)
            ? throw new RequiredFieldException(nameof(password))
            : BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string plainPassword, string hashedPassword)
    {
        return string.IsNullOrWhiteSpace(plainPassword) ? throw new RequiredFieldException(nameof(plainPassword)) :
            string.IsNullOrWhiteSpace(hashedPassword) ? throw new RequiredFieldException(nameof(hashedPassword)) :
            BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }
}