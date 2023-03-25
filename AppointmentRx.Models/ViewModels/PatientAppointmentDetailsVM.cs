using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.ViewModels
{
    public class PatientAppointmentDetailsVM
    {
        public int AppointmentId { get; set; }
        public string? DoctorId { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int? SerialNumber { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int? ChamberId { get; set; }
        public string? ChamberName { get; set; }
        public string? ChamberAddress { get; set; }
        public string? ChamberOpeningTime { get; set; }
        public string? ChamberClosingTime { get; set; }
        public float? Fees { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
