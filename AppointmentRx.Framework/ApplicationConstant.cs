using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Framework
{
    public class ApplicationConstant
    {
        public const string DisplayDateFormat = "dd-MMM-yyyy";
        public const string DisplayShortMonthFormat = "dd MMM yyyy";
        public const int WebTokenExpiryTime = 30;

        public const int AccessTokenExpiryDay = 1;
        public const int RefreshTokenExpiryDay = 30;

    }

    public enum ApplicationRole
    {
        [Description("Doctor")]
        Doctor = 3,
        [Description("Patient")]
        Patient = 5,
        //[Description("Assistant")]
        //Assistant = 4
    }

    //public class LoginProvider
    //{
    //    public const string Phone = "phone";
    //    public const string Facebook = "facebook";
    //    public const string Google = "google";
    //    public const string Apple = "apple";
    //}
    public class DoctorType
    {
        public const string Local = "local";
        public const string International = "international";
        public const string MyDoctors = "mydoctors";
    }
}
