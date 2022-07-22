using System;
using System.Collections.Generic;
using System.Text;

namespace TFLRoadStatus.BL.Model
{
    public class RoadStatusQuery
    {
        public string roadId { get; set; }
        public string apiBaseUri { get; set; }
        public string appId { get; set; }
        public string apiKey { get; set; }
    }
}
