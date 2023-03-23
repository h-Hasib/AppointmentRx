using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class DoctorProfile
    {
        [Key]
        public string Id { get; set; }
        public string? BMDCNumber { get; set; }
        public string? Designation { get; set; } // Make DesignationId from MasterData + Enum
        public string? Department { get; set; }  // Make DesignationId from MasterData + Enum

        public ICollection<Chamber>? Chambers { get; set; }
    }
}
