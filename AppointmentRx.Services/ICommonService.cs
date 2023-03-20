namespace AppointmentRx.Services
{
    public interface ICommonService
    {
        // string GetLoginUserShorName(UserCredential user);
        public int GenerateOtp();
        string ConvertPhoneNumber(string phoneNumber, string countryCode = null);

    }
}
