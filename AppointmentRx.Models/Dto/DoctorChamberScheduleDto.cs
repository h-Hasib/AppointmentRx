using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Models.Dto
{
    public class DoctorChamberScheduleDto
    {
        public string Name { get; set; }
        public float? Fees { get; set; }
        public string? Address { get; set; }
        public string? OpeningTime { get; set; }    //Time handles ranges from 00:00:00 through 23:59:59
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
