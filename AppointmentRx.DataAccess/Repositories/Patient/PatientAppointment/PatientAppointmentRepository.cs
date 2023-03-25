using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Patient.PatientAppointment;
using AppointmentRx.Models.Dto;
using AppointmentRx.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.DataAccess.Repositories.Patient.Appointment
{
    public class PatientAppointmentRepository : IPatientAppointmentRepository
    {
        private readonly PortalDbContext _db;
        private static int _serialNo = 1;

        public PatientAppointmentRepository(PortalDbContext db)
        {
            _db = db;
        }
        public static int GetSerialNumber()
        {
            return _serialNo++;
        }

        public async Task<Entitites.Appointment> CreateAppointment(PatientAppointmentDto request, string patientId)
        {
            var appointment = new Entitites.Appointment
            {
                PatientName = request.PatientName,
                PhoneNumber = request.PhoneNumber,
                Age = request.Age,
                SerialNumber = GetSerialNumber(),
                PatientId = patientId,
                DoctorId = request.DoctorId,
                ChamberId = request.ChamberId,
                AppointmentTime = request.AppointmentTime,
                CreatedAt = DateTime.Now
            };
            await _db.Appointments.AddAsync(appointment);
            if (await _db.SaveChangesAsync() > 0)
                return appointment;
            return null;
        }
        public async Task<List<PatientAppointmentListVM>> GetAllAppointments(string patientId)
        {
            return await (
                from a in _db.Appointments
                join p in _db.PortalUsers on a.DoctorId equals p.Id
                join d in _db.DoctorProfiles on a.DoctorId equals d.Id
                where a.PatientId == patientId
                select new PatientAppointmentListVM
                {
                    AppointmentId = a.Id,
                    DoctorId = a.DoctorId,
                    ChamberId = a.ChamberId,
                    DoctorFirstName = p.FirstName,
                    DoctorLastName = p.LastName,
                    Department = d.Department,
                    AppointmentTime = a.AppointmentTime,
                    SerialNumber = a.SerialNumber,
                    PatientName = a.PatientName,
                    PhoneNumber = a.PhoneNumber,
                    Age = a.Age
                }
            ).ToListAsync();
        }

        public async Task<PatientAppointmentDetailsVM?> GetAppointmentDetails(int appointmentId)
        {
            return await (
                from a in _db.Appointments
                join p in _db.PortalUsers on a.DoctorId equals p.Id
                join d in _db.DoctorProfiles on a.DoctorId equals d.Id
                join c in _db.Chambers on a.ChamberId equals c.Id
                where a.Id == appointmentId
                select new PatientAppointmentDetailsVM
                {
                    AppointmentId = a.Id,
                    DoctorId = a.DoctorId,
                    DoctorFirstName = p.FirstName,
                    DoctorLastName = p.LastName,
                    Department = d.Department,
                    Designation = d.Designation,
                    AppointmentTime = a.AppointmentTime,
                    SerialNumber = a.SerialNumber,
                    PatientName = a.PatientName,
                    PhoneNumber = a.PhoneNumber,
                    Age = a.Age,
                    ChamberId = a.ChamberId,
                    ChamberName = c.Name,
                    ChamberAddress = c.Address,
                    ChamberOpeningTime = c.OpeningTime,
                    ChamberClosingTime = c.ClosingTime,
                    Fees = c.Fees,
                    CreatedAt = a.CreatedAt 
                }
            ).FirstOrDefaultAsync();
        }

        public async Task<List<PatientAppointmentListVM>?> UpcomingAppointments(string patientId)
        {
            return await (
                from a in _db.Appointments
                join p in _db.PortalUsers on a.DoctorId equals p.Id
                join d in _db.DoctorProfiles on a.DoctorId equals d.Id
                where a.PatientId == patientId && a.AppointmentTime.Date > DateTime.Today
                select new PatientAppointmentListVM
                {
                    AppointmentId = a.Id,
                    DoctorId = a.DoctorId,
                    ChamberId = a.ChamberId,
                    DoctorFirstName = p.FirstName,
                    DoctorLastName = p.LastName,
                    Department = d.Department,
                    AppointmentTime = a.AppointmentTime,
                    SerialNumber = a.SerialNumber,
                    PatientName = a.PatientName,
                    PhoneNumber = a.PhoneNumber,
                    Age = a.Age
                }
            ).ToListAsync();
        }
        public async Task<List<PatientAppointmentListVM>?> TodaysAppointments(string patientId)
        {
            return await (
                from a in _db.Appointments
                join p in _db.PortalUsers on a.DoctorId equals p.Id
                join d in _db.DoctorProfiles on a.DoctorId equals d.Id
                where a.PatientId == patientId && a.AppointmentTime.Date == DateTime.Today
                select new PatientAppointmentListVM
                {
                    AppointmentId = a.Id,
                    DoctorId = a.DoctorId,
                    ChamberId = a.ChamberId,
                    DoctorFirstName = p.FirstName,
                    DoctorLastName = p.LastName,
                    Department = d.Department,
                    AppointmentTime = a.AppointmentTime,
                    SerialNumber = a.SerialNumber,
                    PatientName = a.PatientName,
                    PhoneNumber = a.PhoneNumber,
                    Age = a.Age
                }
            ).ToListAsync();
        }
    }
}
