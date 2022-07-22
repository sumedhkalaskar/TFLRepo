using System;
using System.Collections.Generic;
using System.Text;

namespace TFLRoadStatus.BL.Model
{
   public class StatusResponse
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string statusSeverity { get; set; }
        public string statusSeverityDescription { get; set; }
        public string bounds { get; set; }
        public string envelope { get; set; }
        public string url { get; set; }
    }
}
