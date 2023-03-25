using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.DoctorList;
using AppointmentRx.Models;
using AppointmentRx.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Patient.DoctorList
{
    [Route("patient/DoctorInfo")]
    [ApiController]
    public class DoctorInfoController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IDoctorInfoRepository _doctorInfoRepository;
        public DoctorInfoController(
            UserManager<PortalUser> userManager,
            IDoctorInfoRepository doctorListRepository
            )
        {
            _userManager = userManager;
            _doctorInfoRepository = doctorListRepository;
        }

        [HttpGet("doctor-list")]
        [AllowAnonymous]
        public async Task<IActionResult> DoctorList()
        {
            var doctorList = await _doctorInfoRepository.GetDoctorList();
            if (doctorList == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "no doctor found."));
            return Ok(new HttpResponseModel(data: doctorList, success: true, message: "doctor list."));
        }
        [HttpGet("doctor-details")]
        [AllowAnonymous]
        public async Task<IActionResult> DoctorDetails(string doctorId)
        {
            //var doctorId = "cf9f1a54-972a-47f3-9cb0-763206814c86";
            var doctorDetails = await _doctorInfoRepository.GetDoctorDetails(doctorId);
            if(doctorDetails == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "no doctor found."));
            return Ok(new HttpResponseModel(data: doctorDetails, success: true, message: "doctor details."));
        }
    }
}
