namespace AppointmentRx.Models.Dto
{
    public class PatientAppointmentDto
    {
        public string? PatientName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public string? DoctorId { get; set; }
        public int? ChamberId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}
