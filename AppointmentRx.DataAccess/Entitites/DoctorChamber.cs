using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    internal class DoctorChamber
    {
        [Key] 
        public Guid ChamberId { get; set; }
        public string Schedule {get; set;}
        public int Fees { get; set;}

        public ICollection<DoctorProfile> Profile;
        public DoctorAddress DoctorAddress;
        public DoctorScheudle DoctorScheudle;
    }
}
