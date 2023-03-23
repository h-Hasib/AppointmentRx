using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class Chamber
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Fees { get; set; }
        public string? Address { get; set; }
        public string? OpeningTime { get; set; }    //Time handles ranges from 00:00:00 through 23:59:59
        public string? ClosingTime { get; set; }

        [ForeignKey("DoctorProfile")]
        public string DoctorId { get; set; }
        public DoctorProfile? DoctorProfile { get; set; }
        
        public Schedule? Schedule { get; set; }
    }
}
