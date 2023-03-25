using AppointmentRx.Models.ViewModels;

namespace AppointmentRx.DataAccess.Repositories.Patient.DoctorList
{
    public interface IDoctorInfoRepository
    {
        Task<List<DoctorListVM>> GetDoctorList();
        Task<DoctorDetailsVM?> GetDoctorDetails(string id);

    }
}
