using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.FavouriteDoctorRepo;
using AppointmentRx.Models;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.Controllers.Patient.FavouriteDoctor
{
    [Route("patient/favorite-doctor")]
    [ApiController]
    public class FavouriteDoctorController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly IFavouriteDoctorRepository _favouriteDoctorRepository;
        public FavouriteDoctorController(
            UserManager<PortalUser> userManager, IFavouriteDoctorRepository favouriteDoctorRepository)
        {
            _userManager = userManager;
            _favouriteDoctorRepository = favouriteDoctorRepository;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("add")]
        public async Task<IActionResult> AddFavouriteDoctor(string doctorId)
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";
            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var alreadyFavourite = await _favouriteDoctorRepository.AlreadyFavourite(userId, doctorId);

            if (alreadyFavourite == null)
            {
                var response = await _favouriteDoctorRepository.AddFavouriteDoctor(userId, doctorId);

                if (response == null)
                    return BadRequest(new HttpResponseModel(data: null, success: false, message: "add favourite doctor failed."));
                return Ok(new HttpResponseModel(data: response, success: true, message: "added favourite doctor."));
            }
            return Ok(new HttpResponseModel(data: alreadyFavourite, success: true, message: "this doctor already in favourite list."));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-list")]
        public async Task<IActionResult> GetFavouriteDoctor()
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";
            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var response = await _favouriteDoctorRepository.GetAllFavouriteDoctors(userId);

            if (response == null)
                return BadRequest(new HttpResponseModel(data: null, success: false, message: "no data found."));
            return Ok(new HttpResponseModel(data: response, success: true, message: "favourite doctors."));
        }
        [HttpDelete]
        [AllowAnonymous]
        [Route("delete")]
        public async Task<IActionResult> DeleteFavourite(string doctorId)
        {
            var userId = "c02d917d-ef60-45fc-95f5-a19323151f82";
            var patient = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (patient == null)
                return NotFound(new HttpResponseModel(data: null, success: false, message: "user not found."));

            var result = await _favouriteDoctorRepository.DeleteFavouriteDoctor(userId, doctorId);
            if(result.Data == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
