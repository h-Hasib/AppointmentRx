using AppointmentRx.DataAccess.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.User
{
    public interface IUserRepository
    {
        Task<PortalUser> Get(string id);
    }
}
