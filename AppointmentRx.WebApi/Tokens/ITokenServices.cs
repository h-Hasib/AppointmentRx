using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;

namespace AppointmentRx.WebApi.Tokens
{
    public interface ITokenService
    {
        Task<TokenModel> GetTokens(PortalUser user);
        string GetUserNameFromToken(string token);
    }
}
