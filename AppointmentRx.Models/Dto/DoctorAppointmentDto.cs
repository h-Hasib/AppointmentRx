using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.Dto
{
    public class DoctorAppointmentDto
    {
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int? SerialNumber { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? ChamberId { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
