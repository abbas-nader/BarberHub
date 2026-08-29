using BarberHub.Domain.Enums;

namespace BarberHub.Application.Security.JwtToken;

public record TokenClaims(
        long UserId,
        UserRole UserRole,
        long? SalonId
        );