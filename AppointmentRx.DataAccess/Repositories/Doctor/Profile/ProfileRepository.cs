using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly PortalDbContext _dbContext;

        public ProfileRepository(PortalDbContext portalDbContext)
        {
            _dbContext = portalDbContext;
        }
        public async Task<HttpResponseModel> Create(DoctorProfile profile)
        {
            await _dbContext.DoctorProfiles.AddAsync(profile);

            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return new HttpResponseModel(profile);
            }
            return new HttpResponseModel(null, false, "Failed");
        }

        public async Task<HttpResponseModel> Delete(Guid Id)
        {
            var data = await _dbContext.DoctorProfiles.FirstOrDefaultAsync(f => f.UserCredentialId==Id);

            if (data == null)
            {
                return new HttpResponseModel(null, false, "Data is not deleted");
            }
            _dbContext.DoctorProfiles.Remove(data);
            await _dbContext.SaveChangesAsync();
            return new HttpResponseModel(data);
        }

        public async Task<HttpResponseModel> GetAll()
        {
            var all = await _dbContext.DoctorProfiles.ToListAsync();

            return new HttpResponseModel(all);
        }

        public async Task<HttpResponseModel> Update(Guid Id)
        {
            var data = await _dbContext.DoctorProfiles.FirstOrDefaultAsync(x => x.UserCredentialId == Id);

            if (data == null)
            {
                return new HttpResponseModel(null, false, "Not Found");
            }
            _dbContext.DoctorProfiles.Update(data);
            if (await _dbContext.SaveChangesAsync() > 0)
            {
                return new HttpResponseModel(data);
            }
            return new HttpResponseModel(data, false, "not updated");
        }
    }
}
