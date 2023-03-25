using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Patient.FavouriteDoctorRepo
{
    public class FavouriteDoctorRepository : IFavouriteDoctorRepository
    {
        private readonly PortalDbContext _db;
        public FavouriteDoctorRepository(PortalDbContext db)
        {
            _db = db;
        }
        public async Task<FavouriteDoctor> AddFavouriteDoctor(string patientId, string doctorId)
        {
            FavouriteDoctor favouriteDoctor = new FavouriteDoctor {
                PatientId = patientId,
                DoctorId = doctorId
            };
            await _db.FavouriteDoctors.AddAsync(favouriteDoctor);
            if (await _db.SaveChangesAsync() > 0)
                return favouriteDoctor;
            return null;
        }
        public async Task<List<FavouriteDoctorListVM>> GetAllFavouriteDoctors(string patientId)
        {
            return await (
                from f in _db.FavouriteDoctors
                join p in _db.PortalUsers on f.DoctorId equals p.Id
                join d in _db.DoctorProfiles on f.DoctorId equals d.Id
                where f.PatientId == patientId
                select new FavouriteDoctorListVM
                {
                    DoctorId = f.DoctorId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Department = d.Department,
                    Designation = d.Designation
                }
             ).ToListAsync();
        }
        public async Task<HttpResponseModel> DeleteFavouriteDoctor(string patientId, string doctorId)
        {
            var deleteRequest = await _db.FavouriteDoctors.FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);
            if (deleteRequest == null)
                return new HttpResponseModel(data: null, success: false, message: "favourite doctor not found.");
            _db.FavouriteDoctors.Remove(deleteRequest);

            if(await _db.SaveChangesAsync() > 0)
            {
                return new HttpResponseModel(data: deleteRequest, success: true, message: "favourite doctor delete.");
            }
            return new HttpResponseModel(data: null, success: false, message: "delete failed.");

        }
        public async Task<FavouriteDoctor> AlreadyFavourite(string patientId, string doctorId)
        {
            var result = await _db.FavouriteDoctors.FirstOrDefaultAsync(x => x.PatientId == patientId && x.DoctorId == doctorId);
            if (result == null)
                return null;
            return result;
        }
    }
}
