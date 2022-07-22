using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TFLRoadStatus.BL;
using TFLRoadStatus.BL.Interface;
using TFLRoadStatus.BL.Model;
using Xunit;

namespace TFLRoadStatus.Unit.Test
{
    public class RestClientTest
    {        
        private IRestClient restClient;
        private readonly Mock<IDisplay> displayMock;               
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

        public RestClientTest()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            displayMock = new Mock<IDisplay>();
            displayMock.Object.Message = new System.Text.StringBuilder();
           
        }

        [Fact]
        public void When_Execute_Request_With_Correct_Url()
        {

            var requestObj = GetRequestObject("https://api.tfl.gov.uk/", "A2", "ba5924dda8614fbf8cfbe60e31dd9a12", "5edb80b597aa49b88e3fc5fdc0f43e68");

            var expectedRoad = "A2";
            var expectedSeverity = "Good";


            var roadStatus = GetRoadStatus<List<StatusResponse>>(requestObj,true).First();

            Assert.NotNull(roadStatus);
            Assert.Equal(expectedRoad, roadStatus.displayName);
            Assert.Equal(expectedSeverity, roadStatus.statusSeverity);
        }


        [Fact]
        public void When_Execute_Request_With_Wrong_Url()
        {
            var requestObj = GetRequestObject("http://testurl.com/", "A2", "ba5924dda8614fbf8cfbe60e31dd9a12", "5edb80b597aa49b88e3fc5fdc0f43e68");
            this.GetMockedHttpFactoryClient(HttpStatusCode.BadRequest, "");
            restClient = new RestClient(_httpClientFactoryMock.Object);

            Assert.ThrowsAsync<HttpRequestException>(() => restClient.Get(requestObj));
        }

        [Fact]
        public void When_Execute_Request_With_Invalid_Road_Then_Returns_Json()
        {
            var requestObj = GetRequestObject("https://api.tfl.gov.uk/", "A2", "ba5924dda8614fbf8cfbe60e31dd9a12", "5edb80b597aa49b88e3fc5fdc0f43e68");

            var expectedHttpStatus = "NotFound";
            var expectedHttpStatusCode = "404";


            var roadStatus = GetRoadStatus<ErrorResponse>(requestObj,false);

            Assert.NotNull(roadStatus);
            Assert.Equal(expectedHttpStatus, roadStatus.HttpStatus);
            Assert.Equal(expectedHttpStatusCode, roadStatus.HttpStatusCode);
        }

        private T GetRoadStatus<T>(RoadStatusQuery roadStatusQuery, bool isHappyPath)
        {
            if (isHappyPath)
            {
                var responseString = "[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a2\",\"displayName\":\"A2\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.0857,51.44091],[0.17118,51.49438]]\",\"envelope\":\"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\",\"url\":\"\"}]";
                this.GetMockedHttpFactoryClient(HttpStatusCode.OK, responseString);
            }
            else
            {
                var responseString = "{\"$type\":\"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\",\"timestampUtc\":\"2017-11-21T14:37:39.7206118Z\",\"exceptionType\":\"EntityNotFoundException\",\"httpStatusCode\":404,\"httpStatus\":\"NotFound\",\"relativeUri\":\"\",\"message\":\"The following road id is not recognised: A233\"}";
                this.GetMockedHttpFactoryClient(HttpStatusCode.OK, responseString);
            }
           
            restClient = new RestClient(_httpClientFactoryMock.Object);

            var result = restClient.Get(roadStatusQuery).Result;
            return JsonConvert.DeserializeObject<T>(result);
        }
       
        private RoadStatusQuery GetRequestObject(string apiBaseUrl, string roadId, string apiKey, string appId)
        {
            return new RoadStatusQuery
            {
                apiBaseUri = apiBaseUrl,
                roadId = roadId,
                apiKey = apiKey,
                appId = appId
            };

        }
        private void GetMockedHttpFactoryClient(HttpStatusCode code, string content)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = code,
                    Content = new StringContent(content),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        }

    }
}
