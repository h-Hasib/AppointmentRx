using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class Schedule
    {
        [Key]
        public Guid Id { get; set; }
        public string? OpeningTime { get; set; }    //Time handles ranges from 00:00:00 through 23:59:59
        public string? ClosingTime { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }

        [ForeignKey("Chamber")]
        public int ChamberId { get; set; }
        public Chamber? Chamber { get; set; }
    }
}
