using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Models;
using AppointmentRx.Models.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.Controllers.Patient.Profile
{
    [Route("patient/profile")]
    [ApiController]
    public class ProfileQueryController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IPatientProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileQueryController(UserManager<PortalUser> userManager, IMapper mapper, IPatientProfileRepository profileRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        [HttpGet("get-details")]
        [AllowAnonymous]
        public async Task<IActionResult> ProfileDetails()
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var patientDetails = await _profileRepository.GetById(userId);
            if (patientDetails == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var response = new PatientProfileVM
            {
                PatientId = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Avatar = patient.Avatar,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                Occupation = patientDetails.Occupation,
                BloodGroup = patientDetails.BloodGroup,
                DateOfBirth = patientDetails.DateOfBirth,
                Address = patientDetails.Address
            };

            return Ok(new HttpResponseModel(data: response, success: true, message: "user details."));
        }
    }
}