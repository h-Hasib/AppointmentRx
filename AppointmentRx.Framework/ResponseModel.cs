﻿namespace AppointmentRx.Framework
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? ReturnCode { get; set; }

        public long? LongId { get; set; }
    }
}
