using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.Dto
{
    public class DoctorProfileDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Designation { get; set; } // Make DesignationId from MasterData + Enum
        public string? Department { get; set; }  // Make DesignationId from MasterData + Enum
    }
}
