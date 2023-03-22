using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
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
    }
}
