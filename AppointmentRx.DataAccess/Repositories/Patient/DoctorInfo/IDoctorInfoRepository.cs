using AppointmentRx.Models.ViewModels;

namespace AppointmentRx.DataAccess.Repositories.Patient.DoctorInfo
{
    public interface IDoctorInfoRepository
    {
        Task<List<DoctorListVM>> GetDoctorList();
        Task<DoctorDetailsVM?> GetDoctorDetails(string id);
        Task<List<DoctorChemberViewModel>?> DoctorChamberList(string doctorId);
    }
}
