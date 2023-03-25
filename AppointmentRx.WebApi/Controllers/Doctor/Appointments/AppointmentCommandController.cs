using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Doctor.Appointments;
using AppointmentRx.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

namespace AppointmentRx.WebApi.Controllers.Doctor.Appointments
{
    [Route("api/doctor/[controller]/[action]")]
    [ApiController]
    public class AppointmentCommandController : ControllerBase
    {
        public IAppointmentRepository _appointmentRepository { get; set; }

        public AppointmentCommandController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(DoctorAppointmentDto model)
        {
            var data = await _appointmentRepository.Create(model);
            return Ok(data);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int Id,DoctorAppointmentDto model)
        {
            var data = await _appointmentRepository.Update(Id,model);
            return Ok(data);
        }

        [HttpDelete]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _appointmentRepository.Delete(Id);
            return Ok(data);
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var data = await _appointmentRepository.GetList();
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetails(int AppointmentId)
        {
            var data = await _appointmentRepository.GetDetails(AppointmentId);
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UpcomingAppointment()
        {
            var data = await _appointmentRepository.UpcomingAppointment();
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> TodayAppointment()
        {
            var data = await _appointmentRepository.TodayAppointment();
            return Ok(data);
        }

    }
}
