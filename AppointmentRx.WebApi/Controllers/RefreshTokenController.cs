using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.WebApi.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers
{
    [Route("refresh-token")]
    [ApiController]
    public class RefreshTokenController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly ITokenService _tokenService;
        public RefreshTokenController(UserManager<PortalUser> userManager,
         ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostRefreshToken([FromBody] TokenModel tokenData)
        {
            var userId = _tokenService.GetUserNameFromToken(tokenData.AccessToken);
            if (string.IsNullOrEmpty(userId))
                return BadRequest("Invalid Access Token!");
            if (string.IsNullOrEmpty(tokenData.RefreshToken))
                return BadRequest("Refresh Token is required!");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Ok(new { Success = false, Message = "User not found" });
            if (string.IsNullOrEmpty(user.RefreshToken))
                return Ok(new { Success = false, Message = "Session not found. Kindly login." });
            if (!user.RefreshToken.Equals(tokenData.RefreshToken))
                return Ok(new { Success = false, Message = "Invalid Refresh Token!" });
            if (DateTime.UtcNow > user.RefreshTokenExpiresAt)
                return Ok(new { Success = false, Message = "Session is Expired! Please login again." });

            var token = await _tokenService.GetTokens(user);
            return Ok(new
            {
                Success = true,
                Token = token
            });
        }
    }
}
