using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Entitites
{
    internal class DoctorAddress
    {
        [Key]
        public Guid AddressId { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string Division { get; set; }
        public string Area { get; set; }
        public string RoadNo { get; set; }
    }
}
