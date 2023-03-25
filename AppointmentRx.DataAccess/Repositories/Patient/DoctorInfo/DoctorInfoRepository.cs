using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.DataAccess.Repositories.Patient.DoctorInfo
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
        public async Task<List<DoctorChemberViewModel>?> DoctorChamberList(string doctorId)
        {
            return await (
                from c in _db.Chambers
                join s in _db.Schedules on c.Id equals s.ChamberId
                where c.DoctorId == doctorId
                select new DoctorChemberViewModel
                {
                    DoctorId = c.DoctorId,
                    ChamberId = s.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Fees = c.Fees,
                    OpeningTime = c.OpeningTime,
                    ClosingTime = c.ClosingTime,
                    Saturday = s.Saturday,
                    Sunday = s.Sunday,
                    Monday = s.Monday,
                    Tuesday = s.Tuesday,
                    Wednesday = s.Wednesday,
                    Thursday = s.Thursday,
                    Friday = s.Friday,
                }
            ).ToListAsync();
        }
    }
}
