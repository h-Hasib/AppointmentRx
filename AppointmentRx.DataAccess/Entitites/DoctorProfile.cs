using AppointmentRx.DataAccess.Entitites.AuthModel;
using AppointmentRx.DataAccess.Repositories.DoctorAppointment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    public class DoctorProfile
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public string Designation { get; set; }
        public string Department { get; set; }

        public int BMDCNo { get; set; }

        public string Avatar { get; set; }

        public string Speciality { get; set;}

        public string BG { get; set; }

        public DateTime DOB { get; set; }


        [ForeignKey("UserId")]
        public Login Login;

        [ForeignKey("ChamberId")]
        public int ChamberId { get; set; }
        public ICollection<DoctorChamber> Chamber;

        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public ICollection<DoctorAppointment> Appointment;

        [ForeignKey("FavoriteId")]
        public int FavoriteId { get; set; }
        public DoctorFavorite Favorite;



    }
}
