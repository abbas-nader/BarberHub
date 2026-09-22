using BarberHub.Domain.Enums;

namespace BarberHub.Application.Security.Authentication;

public record TokenClaims(
        long UserId,
        UserRole UserRole,
        long? SalonId
        );