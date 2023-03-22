using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;

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
        public async Task<PatientProfile> Create(PatientProfile entity)
        {
            await _db.PatientProfiles.AddAsync(entity);

            if (await _db.SaveChangesAsync() > 0)
                return(entity);

            return null;
        }
        public async Task<PatientProfile> Update(PatientProfile request)
        {
            _db.PatientProfiles.Update(request);

            if (await _db.SaveChangesAsync() > 0)
                return request;
            return null;
        }
    }
}
