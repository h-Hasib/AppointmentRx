using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Framework;
using AppointmentRx.Models;
using AppointmentRx.Services;
using AppointmentRx.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Models.Dto;

namespace AppointmentRx.WebApi.Controllers.Patient.Auth
{
    [Route("patient/auth")]
    [ApiController]
    public class AccountCommandController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly ICommonService _commonService;
        //private readonly OtpConfiguration _otpConfig;
        private readonly IPatientProfileRepository _profileRepository;

        public AccountCommandController(UserManager<PortalUser> userManager, ICommonService commonService,
                                     IPatientProfileRepository profileRepository)
        {
            _userManager = userManager;
            _commonService = commonService;
            //_otpConfig = otpConfig;
            _profileRepository = profileRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                var newUser = await CreateAccount(
                    new PatientRegistrationDto
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                    }
                );
                return Ok(new HttpResponseModel(data: newUser, success: true, message: "new user created."));
            }
            return Ok(new HttpResponseModel(data: user, success: true, message: "login success."));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("createAccount")]
        public async Task<IActionResult> CreateAccount(PatientRegistrationDto request)
        {
            var userEntity = new PortalUser
            {
                //Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
               
                CountryCode = request.CountryCode,
            
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Avatar = request.Avatar,


                CreatedAt = DateTime.UtcNow,
                Otp = _commonService.GenerateOtp(),
                //OtpExpiryAt = DateTime.UtcNow.AddMinutes(_otpConfig.Expiration),
                RoleId = (int)ApplicationRole.Patient
            };
            var userCreateResponse = await _userManager.CreateAsync(userEntity);
            if (!userCreateResponse.Succeeded)
                return BadRequest(new HttpResponseModel(data: userCreateResponse.Errors, success: false, message: "Internal server error."));

            var profile = new PatientProfile
            {
                Id = userEntity.Id
            };
            await _profileRepository.Create(profile);
            //var smsResponse = await SendSignupOtp(phoneNo, userEntity.Otp.ToString());
            return Ok(new { NewUser = true, Message = "User created and OTP sent. OTP is: " + userEntity.Otp });//, smsResponse });
        }
        //private async Task<IActionResult> SendOtpIfExists(PortalUser portalUser)
        //{
        //    try
        //    {
        //        var otp = _commonService.GenerateOtp();
        //        portalUser.Otp = otp;
        //        portalUser.OtpExpiryAt = DateTime.UtcNow.AddMinutes(_otpConfig.Expiration);
        //        await _userManager.UpdateAsync(portalUser);

        //        var smsResponse = await SendSignupOtp(portalUser.UserName, otp.ToString());
        //        return Ok(new { NewUser = false, Message = "OTP sent to your mobile. OTP is: " + otp, smsResponse });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //private async Task<string> SendSignupOtp(string phoneNo, string otp)
        //{
        //    if (string.IsNullOrEmpty(phoneNo))
        //        return string.Empty;
        //    string smsBody = string.Format("Your OTP is {0}.", otp);

        //    return await _smsService.SendSms(phoneNo, smsBody);
        //}
        //[HttpPost]
        //[Route("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    var user = await _userManager.FindByIdAsync(UserId);
        //    if (user == null || user.RoleId != (int)ApplicationRole.Patient)
        //        return NotFound(new HttpResponseModel(null, false, "User not found!"));
        //    user.PhoneNumberConfirmed = false;
        //    user.Otp = null;
        //    user.OtpExpiryAt = null;
        //    user.IsActive = false;
        //    user.IsVerified = false;
        //    user.RefreshToken = null;
        //    user.RefreshTokenExpiresAt = null;
        //    var result = await _userManager.UpdateAsync(user);
        //    if (!result.Succeeded)
        //        return BadRequest(new HttpResponseModel(null, false, "Invalid Operation!"));
        //    return Ok(new HttpResponseModel(null, true, "Successfully Logged Out!"));
        //}
    }
}





