using AppointmentRx.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace AppointmentRx.WebApi.Controllers
{
    [ApiExceptionFilter]
    public abstract class BaseController : Controller
    {
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        protected BaseController() { }
        public string UserId => this.User.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sid).Value;
        public string RoleId => this.User.Identity.GetRoleId();
    }
    public static class CustomClaimTypes
    {
        public const string RoleId = "RoleId";
    }
    public static class IdentityExtensions
    {
        public static string GetRoleId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(CustomClaimTypes.RoleId);

            if (claim == null)
                return string.Empty;

            return claim?.Value ?? string.Empty;
        }

        public static string GetName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }
    }
}
