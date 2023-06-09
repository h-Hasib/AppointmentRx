﻿using AppointmentRx.DataAccess.Repositories.Doctor.Profile;
using AppointmentRx.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Doctor.Profile
{
    [Route("api/doctor/[controller]/[action]")]
    [ApiController]
    public class ProfileCommandController : ControllerBase
    {
        public IDoctorProfileRepository _doctorProfileRepository { get; set; }

        public ProfileCommandController(IDoctorProfileRepository doctorProfileRepository )
        {
            _doctorProfileRepository = doctorProfileRepository;
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Update(/*int Id*/ DoctorProfileDto model)
        {
            var data = await _doctorProfileRepository.Update(model);
            return Ok(data);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var data = await _doctorProfileRepository.GetList();
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetails()
        {
            var data = await _doctorProfileRepository.GetDetails();
            return Ok(data);
        }

    }
}
