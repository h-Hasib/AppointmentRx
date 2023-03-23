using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.DataAccess.Repositories.Patient.DoctorList
{
    public class DoctorInfoRepository : IDoctorInfoRepository
    {
        private readonly PortalDbContext _db;
        public DoctorInfoRepository(PortalDbContext db)
        {
            _db = db;
        }

        public async Task<List<DoctorListVM>> GetDoctorList()
        {
            return await 
            (
                from d in _db.DoctorProfiles
                join p in _db.PortalUsers
                on d.Id equals p.Id
                select new DoctorListVM
                {
                    DoctorId = d.Id,
                    Avatar = p.Avatar,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Designation = d.Designation,
                    Department = d.Department,
                }
            ).ToListAsync();
        }
        public async Task<DoctorDetailsVM?> GetDoctorDetails(string id)
        {
            return await
            (
                from d in _db.DoctorProfiles
                join p in _db.PortalUsers
                on d.Id equals p.Id
                where d.Id == id
                select new DoctorDetailsVM
                {
                    DoctorId = d.Id,
                    Avatar = p.Avatar,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Designation = d.Designation,
                    Department = d.Department,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber
                }
            ).FirstOrDefaultAsync();
        }

    }
}
