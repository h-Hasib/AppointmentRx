using System.Net;

namespace AppointmentRx.Framework.Models
{
    public class ApiError : ApiErrorSlim
    {
        public ApiError(HttpStatusCode status, string message, InternalErrorCode internalError = InternalErrorCode.None, string details = null)
            : base(message, internalError)
        {
            this.StatusCode = status;
            this.Details = details;
        }


        public HttpStatusCode StatusCode { get; }

        public string Details { get; set; }
    }
}
