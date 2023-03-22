using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class DoctorProfile
    {
        [Key]
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BMDCNumber { get; set; }
        public string Designation { get; set; } // Make DesignationId from MasterData + Enum
        public string Department { get; set; }  // Make DesignationId from MasterData + Enum

        [ForeignKey("PortalUser")]
        public Guid PortalUserId { get; set; } //FK from UserCredential
        public PortalUser PortalUser { get; set; }
        public ICollection<Chamber>? Chambers { get; set; }
    }
}
