using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    public class DoctorAppointment
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public int SerialNumber { get; set; }
        public int Cost { get; set; }

        [ForeignKey("ProfileId")]
        public int ProfileId { get; set; }
        public DoctorProfile Profile;

        [ForeignKey("ChamberId")]
        public int ChamberId { get; set; }
        public DoctorChamber Chamber;

    }
}
