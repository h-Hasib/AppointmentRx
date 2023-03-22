using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.DataAccess.Repositories.User;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.Controllers.Patient.Profile
{
    [Route("patient/profile")]
    [ApiController]
    public class ProfileCommandController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IPatientProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProfileCommandController(
            UserManager<PortalUser> userManager, 
            IMapper mapper, 
            IPatientProfileRepository profileRepository,
            IUserRepository userRepository
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        [HttpPut]
        [Route("update")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProfile([FromBody] PatientProfileUpdateDto model)
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var patientDetails = await _profileRepository.GetById(userId);
            if (patientDetails == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));
        
            patient.FirstName = model.FirstName;
            patient.LastName = model.LastName;
            patient.Avatar = model.Avatar;
            patient.PhoneNumber = model.PhoneNumber;
            patientDetails.Occupation = model.Occupation;
            patientDetails.BloodGroup = model.BloodGroup;
            patientDetails.DateOfBirth = model.DateOfBirth;
            patientDetails.Address = model.Address;

            var updatedPatient = await _userRepository.Update(patient);
            var updatedPatientProfile = await _profileRepository.Update(patientDetails);

            if(updatedPatient == null || updatedPatientProfile == null)
            {
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "update failed."));
            }
            return Ok(new HttpResponseModel(data: null, success: true, message: "update success."));

        }
    }
}
