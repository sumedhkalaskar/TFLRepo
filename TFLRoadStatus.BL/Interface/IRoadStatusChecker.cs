using System;
using System.Collections.Generic;
using System.Text;
using TFLRoadStatus.BL.Model;

namespace TFLRoadStatus.BL.Interface
{
    public interface IRoadStatusChecker
    {
        IDisplay Display { get; }
        ErrorResponse errorResponse { get; set; }
        StatusResponse statusResponse { get; set; }
        int GetRoadCurrentStatus(string roadId, string apiBaseUri, string appId, string apiKey);
    }
}
