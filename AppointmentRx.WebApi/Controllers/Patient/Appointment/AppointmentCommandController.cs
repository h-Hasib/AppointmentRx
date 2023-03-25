using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Appointment;
using AppointmentRx.DataAccess.Repositories.Patient.PatientAppointment;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.Controllers.Patient.Appointment
{
    [Route("patient/appointment")]
    [ApiController]
    public class AppointmentCommandController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IPatientAppointmentRepository _appointmentRepository;
        public AppointmentCommandController(UserManager<PortalUser> userManager,
            IPatientAppointmentRepository appointmentRepository)
        {
            _userManager = userManager;
            _appointmentRepository = appointmentRepository;
        }
        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePatientAppointment([FromBody] PatientAppointmentDto request)
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var appointment = await _appointmentRepository.CreateAppointment(request, userId);
            if (appointment == null)
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "appointment create failed."));
            return Ok(new HttpResponseModel(data: appointment, success: true, message: "appointment created."));
        }

    }
}
