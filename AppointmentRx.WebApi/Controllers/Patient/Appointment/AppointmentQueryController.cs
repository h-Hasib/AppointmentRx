using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.Appointment;
using AppointmentRx.Models.Dto;
using AppointmentRx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppointmentRx.DataAccess.Repositories.Patient.PatientAppointment;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentRx.WebApi.Controllers.Patient.Appointment
{
    [Route("patient/appointment")]
    [ApiController]
    public class AppointmentQueryController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IPatientAppointmentRepository _appointmentRepository;
        public AppointmentQueryController(UserManager<PortalUser> userManager,
           IPatientAppointmentRepository appointmentRepository)
        {
            _userManager = userManager;
            _appointmentRepository = appointmentRepository;
        }
        [HttpGet("all-appointment-list")]
        [AllowAnonymous]
        public async Task<IActionResult> AllAppointment()
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var appointments = await _appointmentRepository.GetAllAppointments(userId);
            if (appointments.IsNullOrEmpty())
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "no appointment found."));
            return Ok(new HttpResponseModel(data: appointments, success: true, message: "appointment list."));
        }
        [HttpGet("appointment-details")]
        [AllowAnonymous]
        public async Task<IActionResult> AppointmentDetails(int appointmentId)
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var appointment = await _appointmentRepository.GetAppointmentDetails(appointmentId);
            if (appointment == null)
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "no appointment found."));
            return Ok(new HttpResponseModel(data: appointment, success: true, message: "appointment details."));
        }
        [HttpGet("upcoming-appointment")]
        [AllowAnonymous]
        public async Task<IActionResult> UpcomingAppointment()
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var appointments = await _appointmentRepository.UpcomingAppointments(userId);
            if (appointments.IsNullOrEmpty())
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "no appointment found."));
            return Ok(new HttpResponseModel(data: appointments, success: true, message: "appointment list."));
        }
        [HttpGet("todays-appointment")]
        [AllowAnonymous]
        public async Task<IActionResult> TodaysAppointment()
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";

            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var appointments = await _appointmentRepository.TodaysAppointments(userId);
            if (appointments.IsNullOrEmpty())
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "no appointment found."));
            return Ok(new HttpResponseModel(data: appointments, success: true, message: "appointment list."));
        }

    }
}
