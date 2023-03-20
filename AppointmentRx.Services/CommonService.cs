namespace AppointmentRx.Services
{
    public class CommonService : ICommonService
    {

        public CommonService() { }
        public int GenerateOtp()
        {
            return new Random().Next(1000, 9999);
        }
        public string ConvertPhoneNumber(string phoneNumber, string countryCode = null)
        {
            if (phoneNumber == null)
                return null;
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = phoneNumber.Remove(0, 1);
            }

            if (countryCode != null)
            {
                if (countryCode.StartsWith("+"))
                {
                    countryCode = countryCode.Remove(0, 1);
                }
                phoneNumber = countryCode + phoneNumber;
            }
            return phoneNumber;
        }
    }
}
