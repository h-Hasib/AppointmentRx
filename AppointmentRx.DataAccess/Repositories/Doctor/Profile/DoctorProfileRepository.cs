using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Profile
{
    public class DoctorProfileRepository:IDoctorProfileRepository
    {
        private readonly PortalDbContext _db;
        public DoctorProfileRepository(PortalDbContext db)
        {
            _db = db;
        }

        public async Task<DoctorProfile?> GetById(string id)
        {
            return await _db.DoctorProfiles.FindAsync(id);
        }
        public async Task<HttpResponseModel> Create(DoctorProfile entity)
        {
            await _db.DoctorProfiles.AddAsync(entity);

            if (await _db.SaveChangesAsync() > 0)
                return new HttpResponseModel(entity, true);

            return new HttpResponseModel(entity, false, "Save Failed!");
        }
    }
}
