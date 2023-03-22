using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Framework;
using AppointmentRx.Models;
using AppointmentRx.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppointmentRx.WebApi.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ICipherService _cipherService;
        public TokenService(UserManager<PortalUser> userManager, IConfiguration config, ICipherService cipherService)
        {
            _userManager = userManager;
            _config = config;
            _cipherService = cipherService;
        }

        public string GetUserNameFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;
            var jwtToken = new JwtSecurityToken(token);
            return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sid).Value;
        }

        public async Task<TokenModel> GetTokens(PortalUser user)
        {
            var accessTokenData = await GenerateAccessToken(user);
            var refreshTokenData = await GenerateRefreshToken(user);
            return new TokenModel
            {
                AccessToken = accessTokenData.AccessToken,
                AccessTokenExpiresAt = accessTokenData.AccessTokenExpiresAt,
                RefreshToken = refreshTokenData.RefreshToken,
                RefreshTokenExpiresAt = refreshTokenData.RefreshTokenExpiresAt
            };
        }

        private async Task<TokenModel> GenerateAccessToken(PortalUser user)
        {
            var roleId = user.RoleId;

            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                user.Email is null? null : new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(CustomClaimTypes.RoleId, _cipherService.Encrypt(roleId.ToString()))
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddDays(ApplicationConstant.AccessTokenExpiryDay);

            var token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds
            );
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenModel
            {
                AccessToken = generatedToken,
                AccessTokenExpiresAt = expiresAt,
            };
        }

        private async Task<TokenModel> GenerateRefreshToken(PortalUser user)
        {
            const int length = 80;
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var token = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            var expiresAt = DateTime.Now.AddDays(ApplicationConstant.RefreshTokenExpiryDay);

            if (user.RefreshToken != null) //todo refactor refresh token service with Request.Headers["User-Agent"]
                token = user.RefreshToken;

            user.RefreshToken = token;
            user.RefreshTokenExpiresAt = expiresAt;
            await _userManager.UpdateAsync(user);

            return new TokenModel
            {
                RefreshToken = token,
                RefreshTokenExpiresAt = expiresAt
            };
        }
    }
}
