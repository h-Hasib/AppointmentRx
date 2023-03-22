using AppointmentRx.DataAccess.Repositories.User;
using AppointmentRx.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Doctor.Chamber
{
    [Route("doctor/chambers")]
    [ApiController]
    public class ChamberCommandController : BaseController
    {
        private readonly IUserRepository _userRepository;

        public ChamberCommandController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddChamber()
        {
            var user = await _userRepository.Get(UserId);
            if (user == null)
                return NotFound(new HttpResponseModel(null, false, "User Not Found!"));
            return Ok(new HttpResponseModel(user, true, "User Found!"));
        }
    }
}



