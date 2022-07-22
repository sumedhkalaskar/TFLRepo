using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TFLRoadStatus.BL;
using TFLRoadStatus.BL.Interface;
using TFLRoadStatus.BL.Model;
using Xunit;

namespace TFLRoadStatus.Unit.Test
{
    public class RoadStatusCheckTest
    {
        private string roadId, apiBaseUri, appId, apiKey;
        private readonly Mock<IRestClient> restClientMock;
        private readonly Mock<IDisplay> displayMock;
        private readonly RoadStatusCheckercs roadStatusChecker;       
        private int ExpectedExitCode;
        private readonly Mock<IHttpClientFactory> clientFactoryMock;

        public RoadStatusCheckTest()
        {
            clientFactoryMock = new Mock<IHttpClientFactory>();            
            restClientMock = new Mock<IRestClient>();
            displayMock = new Mock<IDisplay>();
            displayMock.Object.Message = new System.Text.StringBuilder();
            roadStatusChecker = new RoadStatusCheckercs(restClientMock.Object, displayMock.Object);
        }



        [Fact]
        public void When_RoadStatus_Valid_Request_Is_Executed_Returns_RoadStatus()
        {
            roadId = "A2";
            ExpectedExitCode = 0;
            string expected = getRoadStatusResponse(true);
            restClientMock.Setup(_ => _.StatusCode).Returns(HttpStatusCode.OK);
            restClientMock.Setup(_ => _.Get(It.IsAny<RoadStatusQuery>())).Returns(Task.FromResult(expected));
            var returnCode = this.ExecutMethod();

            Assert.Equal(ExpectedExitCode, returnCode);

            displayMock.Verify(c => c.AddHeader(this.roadId), Times.Once);
            displayMock.Verify(c => c.PrintStatus(), Times.Once);
        }

        [Fact]
        public void When_RoadStatus_Invalid_Request_Is_Executed_Returns_NotFound()
        {
            roadId = "A223";
            ExpectedExitCode = 1;
            string expected = getRoadStatusResponse(false);
            restClientMock.Setup(c => c.StatusCode).Returns(HttpStatusCode.NotFound);
            restClientMock.Setup(re => re.Get(It.IsAny<RoadStatusQuery>())).Returns(Task.FromResult(expected));

            var returnCode = this.ExecutMethod();

            Assert.Equal(ExpectedExitCode, returnCode);
            displayMock.Verify(c => c.AddError(this.roadId), Times.Once);
            displayMock.Verify(c => c.PrintStatus(), Times.Once);
        }

        private int ExecutMethod()
        {           
            this.apiKey = "ba5924dda8614fbf8cfbe60e31dd9a12";
            this.appId = "5edb80b597aa49b88e3fc5fdc0f43e68";
            this.apiBaseUri = "https://api.tfl.gov.uk/";
            return roadStatusChecker.GetRoadCurrentStatus(this.roadId, apiBaseUri, appId, apiKey);
        }
        private string getRoadStatusResponse(bool isHappyPath)
        {
            string res = isHappyPath
                ? "[{\"$type\":\"Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities\",\"id\":\"a2\",\"displayName\":\"A2\",\"statusSeverity\":\"Good\",\"statusSeverityDescription\":\"No Exceptional Delays\",\"bounds\":\"[[-0.0857,51.44091],[0.17118,51.49438]]\",\"envelope\":\"[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]\",\"url\":\"\"}]"
                : "{\"$type\":\"Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities\",\"timestampUtc\":\"2017-11-21T14:37:39.7206118Z\",\"exceptionType\":\"EntityNotFoundException\",\"httpStatusCode\":404,\"httpStatus\":\"NotFound\",\"relativeUri\":\"\",\"message\":\"The following road id is not recognised: A233\"}";
            return res;
        }
    }
}

