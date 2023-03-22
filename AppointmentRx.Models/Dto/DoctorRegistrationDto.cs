using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.Dto
{
    public class DoctorRegistrationDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? CountryCode { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
    }
}
