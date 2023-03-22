using AppointmentRx.DataAccess.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly PortalDbContext _db;
        public UserRepository(PortalDbContext db)
        {
            _db = db;   
        }
        public async Task<PortalUser> Get(string id)
        {
            return await _db.PortalUsers.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<PortalUser> Update(PortalUser user)
        {
            _db.PortalUsers.Update(user);

            if (await _db.SaveChangesAsync() > 0)
                return user;
            return null;
        }
    }
}
