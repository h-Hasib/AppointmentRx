using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    internal class DoctorProfile
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public string Designation { get; set; }

        public int BMDCNo { get; set; }

        public string Avatar { get; set; }

        public string Speciality { get; set;}

        public string BG { get; set; }

        public DateTime DOB { get; set; }

        [ForeignKey("ChamberId")]
        public int ChamberId { get; set; }
        public DoctorChamber Chamber;

        [ForeignKey("AddressId")]
        public DoctorAddress Address;


        public ICollection<DoctorAppointment> Appointment;

        public ICollection<DoctorFavorite> Favorite;



    }
}
