using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class PatientProfile
    {
        [Key]
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Occupation { get; set; }
        public string? BloodGroup { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }

        [ForeignKey("PortalUser")]
        public Guid PortalUserId { get; set; } //FK from PortalUser
        public PortalUser? PortalUser { get; set; }
    }
}
