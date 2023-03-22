using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Repositories.Patient.Profile
{
    public interface IPatientProfileRepository
    {
        public Task<PatientProfile?> GetById(string id);
        public Task<HttpResponseModel> Create(PatientProfile entity);
    }
}
