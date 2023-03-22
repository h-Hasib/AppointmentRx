using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using AppointmentRx.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Profile
{
    public class DoctorProfileRepository:IDoctorProfileRepository
    {
        private readonly UserManager<PortalUser> _userManager;
        private readonly PortalDbContext _dbContext;
        public DoctorProfileRepository(PortalDbContext db, UserManager<PortalUser> userManager)
        {
            _dbContext = db;
            _userManager = userManager;
        }

        public async Task<DoctorProfile?> GetById(string id)
        {
            return await _dbContext.DoctorProfiles.FindAsync(id);
        }
        public async Task<HttpResponseModel> Create(DoctorProfile entity)
        {
            await _dbContext.DoctorProfiles.AddAsync(entity);

            if (await _dbContext.SaveChangesAsync() > 0)
                return new HttpResponseModel(entity, true);

            return new HttpResponseModel(entity, false, "Save Failed!");
        }

        public async Task<HttpResponseModel> Update(int Id, DoctorProfileDto model)
        {
            var doctorId = await _dbContext.DoctorProfiles.FindAsync(Id);
            var portalId = await _dbContext.PortalUsers.FirstOrDefaultAsync(f => f.Id == doctorId.Id);

            if (doctorId == null && portalId == null)
            {
                return new HttpResponseModel(null, false, "Doctor Not Found");
            }

            portalId.FirstName = model.FirstName;
            portalId.LastName = model.LastName;
            portalId.Avatar = model.Avatar;

            doctorId.Designation = model.Designation;
            doctorId.Department = model.Department;

            return new HttpResponseModel(model);
        }

        public async Task<HttpResponseModel> Delete(int Id)
        {
            var doctorId = await _dbContext.DoctorProfiles.FindAsync(Id);
            var portalId = await _dbContext.PortalUsers.FirstOrDefaultAsync(f => f.Id == doctorId.Id);

            if (doctorId == null && portalId == null)
            {
                return new HttpResponseModel(null, false, "Doctor Not Found");
            }

            _dbContext.DoctorProfiles.Remove(doctorId);
            await _dbContext.SaveChangesAsync();

            return new HttpResponseModel(doctorId);
        }

        public async Task<HttpResponseModel> GetDetails()
        {
            var doctorId = "881f6999-936f-48a4-abdd-5a1eaad3e16f";

            /*            var doctor = await _dbContext.DoctorProfiles.FindAsync(doctorId);
                        if (doctor == null)
                        {
                            new HttpResponseModel(null, false, "Doctor Not Found");
                        }

                        var doctordetails = await _dbContext.PortalUsers.FindAsync(doctor);
                        if (doctordetails == null)
                        {
                            new HttpResponseModel(null, false, "Doctor Not Found");
                        }*/

            var doctromanager = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == doctorId);
            if (doctromanager == null)
            {
                new HttpResponseModel(null, false, "Doctor Not Found");
            }
            var doctor = await _dbContext.DoctorProfiles.FindAsync(doctromanager.Id);

            var data = new DoctorProfileViewModel()
            {

                FirstName= doctromanager.FirstName,
                LastName= doctromanager.LastName,
                RoleId=doctromanager.RoleId,
                Department=doctor.Department,
                Designation=doctor.Designation,
                BMDCNumber=doctor.BMDCNumber,
                Username= doctromanager.UserName,
                ContactNo= doctromanager.PhoneNumber
            };
            return new HttpResponseModel(data);
        }


        public async Task<HttpResponseModel> GetList()
        {
          var data = (from d in _dbContext.DoctorProfiles
                       join p in _dbContext.PortalUsers on d.Id equals p.Id
                       select new DoctorProfileViewModel()
                       {
                           
                           BMDCNumber = d.BMDCNumber,
                           Designation = d.Designation,
                           Department = d.Department,
                           FirstName= p.FirstName, LastName= p.LastName,
                           RoleId=p.RoleId
                       }).ToList();
            return new HttpResponseModel(data);
        }

    }
}
