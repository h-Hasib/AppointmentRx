using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.ViewModels
{
    public class DoctorChemberViewModel
    {
        public string? DoctorId { get; set; }
        public int ChamberId { get; set; }
        public string Name { get; set; }
        public float? Fees { get; set; }
        public string? Address { get; set; }

        public string? OpeningTime { get; set; }
        public string? ClosingTime { get; set; }

        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }
        public bool? Monday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Friday { get; set; }

    }
}
