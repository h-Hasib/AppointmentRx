using AppointmentRx.DataAccess.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.WebApi.Controllers.Patient.DoctorList
{
    [Route("patient/DoctorList")]
    [ApiController]
    public class DoctorListController : BaseController
    {
        private readonly UserManager<PortalUser> _userManager;

        public DoctorListController(UserManager<PortalUser> userManager)
        {
            _userManager = userManager;
        }

    }
}
