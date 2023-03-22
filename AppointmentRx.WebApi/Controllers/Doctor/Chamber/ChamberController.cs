using AppointmentRx.DataAccess.Repositories.Doctor.Chamber;
using AppointmentRx.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Doctor.Chamber
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChamberController : ControllerBase
    {
        public IChamberRepositoy _chamberRepositoy { get; set; }

        public ChamberController(IChamberRepositoy chamberRepositoy) 
        {
            _chamberRepositoy = chamberRepositoy;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateChamber(DoctorChamberScheduleDto model)
        {
            var data = await _chamberRepositoy.Create(model);
            return Ok(data);
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateChamber(int Id, DoctorChamberScheduleDto model)
        {
            var data = await _chamberRepositoy.Update(Id,model);
            return Ok(data);
        }

        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int Id)
        {
            var data = await _chamberRepositoy.Delete(Id);
            return Ok(data);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var data = await _chamberRepositoy.GetList();
            return Ok(data);
        }

    }
}
