using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class PatientProfile
    {
        [Key]
        public string Id { get; set; }
        public string? Occupation { get; set; }
        public string? BloodGroup { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
    }
}
