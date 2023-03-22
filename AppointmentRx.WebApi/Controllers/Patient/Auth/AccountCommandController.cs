using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Framework;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using AppointmentRx.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Patient.Auth
{
    [Route("patient/auth")]
    [ApiController]
    public class AccountCommandController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly ICommonService _commonService;
        private readonly IPatientProfileRepository _profileRepository;

        public AccountCommandController(UserManager<PortalUser> userManager, ICommonService commonService,
                                     IPatientProfileRepository profileRepository)
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
                    new PatientRegistrationDto
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
        public async Task<IActionResult> CreateAccount(PatientRegistrationDto request)
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
                RoleId = (int)ApplicationRole.Patient
            };
            var userCreateResponse = await _userManager.CreateAsync(userEntity);
            if (!userCreateResponse.Succeeded)
                return BadRequest(new HttpResponseModel(data: userCreateResponse.Errors, success: false, message: "Internal server error."));

            var profile = new PatientProfile
            {
                Id = userEntity.Id
            };
            var result = await _profileRepository.Create(profile);
            if(result == null)
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "account create failed."));
            
            return Ok(new HttpResponseModel(data: profile, success: true, message:"account create success."));
        }
    }
}





