namespace AppointmentRx.Models
{
    public class HttpResponseModel
    {
        public object? Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public HttpResponseModel(object? data = null, bool success = true, string message = "Success")
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }
}
