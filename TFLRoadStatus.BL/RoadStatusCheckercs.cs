using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TFLRoadStatus.BL.Interface;
using TFLRoadStatus.BL.Model;

namespace TFLRoadStatus.BL
{
    public class RoadStatusCheckercs : IRoadStatusChecker
    {
        private readonly IRestClient _restClient;
        public IDisplay Display { get; }
        public RoadStatusCheckercs(IRestClient restClient,IDisplay display)
        {
            _restClient = restClient;
            Display = display;
        }
       

        public ErrorResponse errorResponse { get; set; }
        public StatusResponse statusResponse { get; set; }       

        public int GetRoadCurrentStatus(string roadId, string apiBaseUri, string appId, string apiKey)
        {
            try {
                var roadStatusQuery = new RoadStatusQuery
                {
                    apiBaseUri = apiBaseUri,
                    roadId = roadId,
                    apiKey = apiKey,
                    appId = appId
                };
                var roadStatus = _restClient.Get(roadStatusQuery).Result;

                if (!string.IsNullOrEmpty(roadStatus?.Trim()))
                {
                    if (_restClient.StatusCode == HttpStatusCode.OK)
                    {
                        statusResponse = JsonConvert.DeserializeObject<List<StatusResponse>>(roadStatus)?.First();
                        PrintStatus();

                        return 0;
                    }
                    else if (_restClient.StatusCode == HttpStatusCode.NotFound)
                    {
                        errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(roadStatus);
                        PrintError(roadId);

                        return 1;
                    }
                }
            }
            catch (Exception ex ) {
                /* custome exception can be used by inheriting Exception class if detailed exceptions required. */
                throw ex;
            }

            return -1;

        }

        private void PrintError(string road)
        {
            Display.AddError(road);
            Display.PrintStatus();
        }

        private void PrintStatus()
        {
            Display.AddHeader(statusResponse.displayName);
            Display.AddRoadStatusText("statusSeverity", statusResponse.statusSeverity);
            Display.AddRoadStatusText("statusSeverityDescription", statusResponse.statusSeverityDescription);
            Display.PrintStatus();
        }
    }
}
