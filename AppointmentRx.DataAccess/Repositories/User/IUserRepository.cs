using AppointmentRx.DataAccess.Entitites;

namespace AppointmentRx.DataAccess.Repositories.User
{
    public interface IUserRepository
    {
        Task<PortalUser> Get(string id);
        Task<PortalUser> Update(PortalUser user);
    }
}
