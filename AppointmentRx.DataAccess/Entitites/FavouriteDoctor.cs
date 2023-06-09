﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class FavouriteDoctor
    {
        [Key]
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
    }
}
