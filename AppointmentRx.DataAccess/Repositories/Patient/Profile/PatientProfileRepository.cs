using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Patient.Profile
{
    public class PatientProfileRepository : IPatientProfileRepository
    {
        private readonly PortalDbContext _db;
        public PatientProfileRepository(PortalDbContext db)
        {
            _db = db;
        }

        public async Task<PatientProfile?> GetById(string id)
        {
            return await _db.PatientProfiles.FindAsync(id);
        }
        public async Task<HttpResponseModel> Create(PatientProfile entity)
        {
            await _db.PatientProfiles.AddAsync(entity);

            if (await _db.SaveChangesAsync() > 0)
                return new HttpResponseModel(entity, true);

            return new HttpResponseModel(entity, false, "Save Failed!");
        }
    }
}
