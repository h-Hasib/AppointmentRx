using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    internal class DoctorAppointment
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public string ScheduleTime { get; set; }
        public int SerialNumber { get; set; }
        public int Cost { get; set; }
        public ICollection<DoctorProfile> Profile { get; set; }

    }
}
