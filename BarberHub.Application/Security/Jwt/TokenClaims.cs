using BarberHub.Domain.Enums;

namespace BarberHub.Application.Security.Jwt;

public record TokenClaims(
        long UserId,
        UserRole UserRole,
        long? SalonId
        );