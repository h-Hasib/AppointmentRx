using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentRx.DataAccess.Entitites
{
    public class UserCredential : IdentityUser
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string CountryCode { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Otp { get; set; }
        public DateTime OtpExpiryAt { get; set; }
        public bool IsVerified { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
    }
}
