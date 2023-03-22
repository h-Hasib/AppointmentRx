using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Doctor.Profile;
using AppointmentRx.Framework;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using AppointmentRx.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Doctor.Auth
{
    [Route("doctor/auth")]
    [ApiController]
    public class AccountCommandController : ControllerBase
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly ICommonService _commonService;
        private readonly IDoctorProfileRepository _profileRepository;

        public AccountCommandController(UserManager<PortalUser> userManager, ICommonService commonService,
                                     IDoctorProfileRepository profileRepository)
        {
            _userManager = userManager;
            _commonService = commonService;
            _profileRepository = profileRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                var newUser = await CreateAccount(
                    new DoctorRegistrationDto
                    {
                        Email = model.Email,
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
                UserName = request.Email,
                PasswordHash = request.Password,
                CountryCode = request.CountryCode,
                PhoneNumber = request.ContactNo,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Avatar = request.Avatar,
                CreatedAt = DateTime.UtcNow,
                Otp = _commonService.GenerateOtp(),
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
            return Ok(new { NewUser = true, Message = "User created and OTP sent. OTP is: " + userEntity.Otp });
        }
    }
}
