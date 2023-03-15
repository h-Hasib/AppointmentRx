using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("AddressId")]
        public int AddressId { get; set; }
        public DoctorAddress Address;

        [ForeignKey("ScheudleId")]
        public DoctorScheudle Scheudle;
    }
}
