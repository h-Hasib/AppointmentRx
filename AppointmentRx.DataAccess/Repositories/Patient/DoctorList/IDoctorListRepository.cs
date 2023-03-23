using AppointmentRx.Models.ViewModels;

namespace AppointmentRx.DataAccess.Repositories.Patient.DoctorList
{
    public interface IDoctorListRepository
    {
        Task<List<DoctorListVM>> GetDoctorList();
    }
}
