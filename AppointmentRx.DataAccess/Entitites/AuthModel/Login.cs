using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites.AuthModel
{
    public class Login
    {
        [Key]
        public Guid LId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public DoctorProfile Profile;

/*        [ForeignKey("RId")]
        public int RId;*/
        public Registration Registration;
    }
}
