using AutoMapper;
using HotelListingAPI.Contracts;
using HotelListingAPI.Data;
using HotelListingAPI.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HotelListingAPI.Repository
{
    public class AuthManager : IAuthManager
    {
        private const string LoginProvider = "HotelListingApi";
        private const string RefreshToken = "RefreshToken";
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = user.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return result.Errors;
        }

        public async Task<AuthResponseDto?> Login(LoginDto user)
        {

            var existUser = await _userManager.FindByEmailAsync(user.Email);
            if (existUser == null)
            {
                return null;
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(existUser, user.Password);

            if (!isValidPassword)
            {
                return null;
            }

            var token = await GenerateToken(existUser);
            return new AuthResponseDto
            {
                Token = token,
                UserId = existUser.Id,
                RefreshToken = await CreateRefreshToken(existUser),
            };
        }

        public async Task<string> CreateRefreshToken(ApiUser user)
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, LoginProvider, RefreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, LoginProvider, RefreshToken);
            await _userManager.SetAuthenticationTokenAsync(user, LoginProvider, RefreshToken, newRefreshToken);
            return newRefreshToken;
        }

        public async Task<AuthResponseDto?> VerifyRefreshToken(AuthResponseDto body)
        {
            var jwtSecutiryTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecutiryTokenHandler.ReadJwtToken(body.Token);

            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (username == null)
            {
                return null;
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null || user.Id != body.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(user, LoginProvider, RefreshToken, body.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken(user);
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = user.Id,
                    RefreshToken = await CreateRefreshToken(user),
                };

            }

            await _userManager.UpdateSecurityStampAsync(user);

            return null;
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtSettings:DurationInDays"])
                ),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
