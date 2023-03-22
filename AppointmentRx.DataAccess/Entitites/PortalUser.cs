using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AppointmentRx.DataAccess.Entitites
{
    public class PortalUser : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ContactNo { get; set; }
        public string Email { get; set; }
        public string? CountryCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Provider { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? FcmToken { get; set; }
        public int? Otp { get; set; }
        public DateTime? OtpExpiryAt { get; set; }
        public bool? IsVerified { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }

        public PatientProfile PatientProfile { get; set; }
        public DoctorProfile DoctorProfile { get; set; }
    }
}
