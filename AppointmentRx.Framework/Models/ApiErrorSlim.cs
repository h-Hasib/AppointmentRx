using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.Framework.Models
{
    public class ApiErrorSlim
    {
        internal ApiErrorSlim(string message, InternalErrorCode internalError = InternalErrorCode.None)
        {
            this.Message = message;
            this.InternalErrorCode = internalError;
        }
        public string Message { get; }

        public InternalErrorCode InternalErrorCode { get; }
    }
}
