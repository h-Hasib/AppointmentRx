﻿using System.ComponentModel.DataAnnotations;

namespace AppointmentRx.Models
{
    public class LoginModel
    {
        [Required]
        public string PhoneNo { get; set; }
    }
    public class ChangePasword
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string currentPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
    }
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? AccessTokenExpiresAt { get; set; }
        public DateTime? RefreshTokenExpiresAt { get; set; }
    }
    public class ChangePass
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
