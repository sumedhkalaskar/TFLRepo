using System;
using System.Collections.Generic;
using System.Text;

namespace TFLRoadStatus.BL.Model
{
    public class ErrorResponse
    {
        public string TimestampUtc { get; set; }
        public string ExceptionType { get; set; }
        public string HttpStatusCode { get; set; }
        public string HttpStatus { get; set; }
        public string RelativeUri { get; set; }
        public string Message { get; set; }
    }
}

