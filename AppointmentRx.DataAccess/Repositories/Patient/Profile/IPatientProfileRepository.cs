using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Models;
using AppointmentRx.Models.Dto;

namespace AppointmentRx.DataAccess.Repositories.Patient.Profile
{
    public interface IPatientProfileRepository
    {
        Task<PatientProfile?> GetById(string id);
        Task<PatientProfile> Create(PatientProfile entity);
        Task<PatientProfile> Update(PatientProfile request);
    }
}
