using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    public class DoctorFavorite
    {
        [Key]
        public Guid FavoriteId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }


        public ICollection<DoctorProfile> Profile;
    }
}
