using AppointmentRx.Models.Dto;
using AppointmentRx.Models.ViewModels;

namespace AppointmentRx.DataAccess.Repositories.Patient.PatientAppointment
{
    public interface IPatientAppointmentRepository
    {
        Task<Entitites.Appointment> CreateAppointment(PatientAppointmentDto request, string patientId);
        Task<List<PatientAppointmentListVM>> GetAllAppointments(string patientId);
        Task<List<PatientAppointmentListVM>?> UpcomingAppointments(string patientId);
        Task<List<PatientAppointmentListVM>?> TodaysAppointments(string patientId);
        Task<PatientAppointmentDetailsVM?> GetAppointmentDetails(int appointmentId);
    }
}
