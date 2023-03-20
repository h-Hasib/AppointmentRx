using AppointmentRx.DataAccess.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.Controllers
{
    [Route("user-claim")]
    public class UserClaimController : BaseController
    {
        private readonly UserManager<UserCredential> _userManager;
        public UserClaimController(UserManager<UserCredential> userManager)
        {
            _userManager = userManager;
        }

        //[HttpGet]
        //[Route("get-claim")]
        //public async Task<IActionResult> GetUserDetails()
        //{
        //    var user = await _userManager.Users.Where(x => x.Id == UserId)
        //        .Select(x => new
        //        {
        //            x.UserName,
        //            x.Avatar,
        //            x.PhoneNumber,
        //            x.FullName,
        //            x.FirstName,
        //            x.LastName,
        //            x.RoleId,
        //            x.IsVerified
        //        })
        //        .AsSplitQuery()
        //        .SingleOrDefaultAsync();

        //    if (user == null)
        //        return Ok(new { Success = false });

        //    return Ok(new
        //    {
        //        Success = true,
        //        User = user
        //    });
        //}
    }
}
