using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class Chamber
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Fees { get; set; }
        public string Address { get; set; }

        [ForeignKey("DoctorProfile")]
        public int DoctorId { get; set; }
        public DoctorProfile? DoctorProfile { get; set; }

        [ForeignKey("Schedule")]
        public Guid ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }
    }
}
