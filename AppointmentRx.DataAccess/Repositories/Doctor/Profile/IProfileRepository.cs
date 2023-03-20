using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Doctor.Profile
{
    public interface IProfileRepository
    {
        public Task<HttpResponseModel> Update(Guid Id);
        public Task<HttpResponseModel> Create(DoctorProfile doctorProfile);
        public Task<HttpResponseModel> Delete(Guid Id);
        public Task<HttpResponseModel> GetAll();
    }
}
