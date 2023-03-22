﻿using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Framework;
using AppointmentRx.Models.Dto;
using AppointmentRx.Models;
using AppointmentRx.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AppointmentRx.DataAccess.Repositories.Doctor.Profile;

namespace AppointmentRx.WebApi.Controllers.Doctor.Auth
{
    [Route("doctor/auth")]
    [ApiController]
    public class AccountCommandController : ControllerBase
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly ICommonService _commonService;
        //private readonly OtpConfiguration _otpConfig;
        private readonly IDoctorProfileRepository _profileRepository;

        public AccountCommandController(UserManager<PortalUser> userManager, ICommonService commonService,
                                     IDoctorProfileRepository profileRepository)
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
                    new DoctorRegistrationDto
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
        public async Task<IActionResult> CreateAccount(DoctorRegistrationDto request)
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
                RoleId = (int)ApplicationRole.Doctor
            };
            var userCreateResponse = await _userManager.CreateAsync(userEntity);
            if (!userCreateResponse.Succeeded)
                return BadRequest(new HttpResponseModel(data: userCreateResponse.Errors, success: false, message: "Internal server error."));

            var profile = new DoctorProfile
            {
                Id = userEntity.Id
            };
            await _profileRepository.Create(profile);
            //var smsResponse = await SendSignupOtp(phoneNo, userEntity.Otp.ToString());
            return Ok(new { NewUser = true, Message = "User created and OTP sent. OTP is: " + userEntity.Otp });//, smsResponse });
        }
    }
}