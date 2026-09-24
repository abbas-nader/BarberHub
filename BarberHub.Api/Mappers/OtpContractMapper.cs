using BarberHub.Api.Contracts.Otp;
using BarberHub.Application.DTOs.Otp;

namespace BarberHub.Api.Mappers;

public static class OtpContractMapper
{
    public static VerifyOtpDto ToDto(this VerifyOtpRequest request) => new(request.Code);
}