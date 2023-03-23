using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.ViewModels
{
    public class DoctorProfileViewModel
    {
        public string Username { get; set; }
        public string ContactNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? BMDCNumber { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
        public int? RoleId { get; set; }
    }
}
