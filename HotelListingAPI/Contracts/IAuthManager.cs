using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace HotelListingAPI.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(RegisterDto user);
    Task<AuthResponseDto?> Login(LoginDto user);
    Task<string> CreateRefreshToken(ApiUser user);
    Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto body);
}