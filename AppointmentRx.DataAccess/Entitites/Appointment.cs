using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int? SerialNumber { get; set;}
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? ChamberId { get; set; }

        public DateTime AppointmentTime { get; set; }
        public DateTime CreatedAt { get; set; } //DateTime.Now
    }
}
