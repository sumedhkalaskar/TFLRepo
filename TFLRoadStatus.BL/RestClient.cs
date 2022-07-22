using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TFLRoadStatus.BL.Interface;
using TFLRoadStatus.BL.Model;

namespace TFLRoadStatus.BL
{
    public class RestClient : IRestClient
    {        
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpStatusCode StatusCode { get;  set; }
        public RestClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get(RoadStatusQuery roadStatusQuery)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new System.Uri(CreateUrl(roadStatusQuery)),
                Method = HttpMethod.Get,
                Headers =
                {
                    { System.Net.HttpRequestHeader.ContentType.ToString(), "application/json" }
                }

            };
            var client = _httpClientFactory.CreateClient();
            client.Timeout = Timeout.InfiniteTimeSpan;
            // ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12;
            var responseMessage = await client.SendAsync(request);

            this.StatusCode = responseMessage.StatusCode;

            if (responseMessage.StatusCode != HttpStatusCode.OK &&
                    responseMessage.StatusCode != HttpStatusCode.NotFound)
            {
                throw new HttpRequestException($"Error request http status code {responseMessage.StatusCode}");
            }

            string content = await responseMessage.Content.ReadAsStringAsync();

            return content;
        }

        private string CreateUrl(RoadStatusQuery roadStatusQuery)
        {
            return string.Format(roadStatusQuery.apiBaseUri + "Road/{0}?app_id={1}&app_key={2}", roadStatusQuery.roadId, roadStatusQuery.appId, roadStatusQuery.apiKey);
        }


    }
}
