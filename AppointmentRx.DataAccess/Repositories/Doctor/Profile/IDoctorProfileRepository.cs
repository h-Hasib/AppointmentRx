using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Profile
{
    public interface IDoctorProfileRepository
    {
        public Task<DoctorProfile?> GetById(string id);
        public Task<HttpResponseModel> Create(DoctorProfile entity);
        public Task<HttpResponseModel> Delete(int Id);
        public Task<HttpResponseModel> Update(int Id, DoctorProfileDto model);

        public Task<HttpResponseModel> GetDetails();
        public Task<HttpResponseModel> GetList();

    }
}
