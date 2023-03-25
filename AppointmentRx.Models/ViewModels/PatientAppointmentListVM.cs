using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.ViewModels
{
    public class PatientAppointmentListVM
    {
        public int AppointmentId { get; set; }
        public string? DoctorId { get; set; }
        public int? ChamberId { get; set; }
        public string? DoctorFirstName { get; set; }
        public string? DoctorLastName { get; set; }
        public string? Department { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int? SerialNumber { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
    }
}
