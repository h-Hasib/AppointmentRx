using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.ViewModels
{
    public class DoctorAppointmentViewModelList
    {
        public int? Id { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int? SerialNumber { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
