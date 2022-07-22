using System;
using System.Collections.Generic;
using System.Text;
using TFLRoadStatus.BL;
using TFLRoadStatus.BL.Interface;
using Xunit;

namespace TFLRoadStatus.Unit.Test
{
    public class DisplayTest
    {
        private readonly IDisplay _display;
        public DisplayTest()
        {
            _display = new Display();
        }

        [Fact]
        public void When_AddHeader_Is_Executed_Returns_AddedText()
        {
            string text = "A2";
            string expectedText = "The status of the A2 is as follows\r\n";

            _display.AddHeader(text);

            var content = _display.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_ReportConstant_statusSeverity_Is_Executed_Returns_AddedText()
        {
            string expectedText = "Road Status";

            string statusSeverity = _display.ReportConstants["statusSeverity"];

            Assert.Equal(expectedText, statusSeverity);
        }

        [Fact]
        public void When_ReportConstant_statusSeverityDescription_Is_Executed_Returns_AddedText()
        {
            string expectedText = "Road Status Description";

            string statusSeverityDescription = _display.ReportConstants["statusSeverityDescription"];

            Assert.Equal(expectedText, statusSeverityDescription);
        }

        [Fact]
        public void When_AddHeaderText_statusSeverity_Is_Executed_Returns_AddedText()
        {
            string key = "statusSeverity";
            string text = "Good";
            string expectedText = "\tRoad Status is Good\r\n";
            _display.AddRoadStatusText(key, text);

            var content = _display.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_AddHeaderText_statusSeverityDescription_Is_Executed_Returns_AddedText()
        {
            string key = "statusSeverityDescription";
            string text = "No Exceptional Delays";
            string expectedText = "\tRoad Status Description is No Exceptional Delays\r\n";
            _display.AddRoadStatusText(key, text);

            var content = _display.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_AddError_Is_Executed_Returns_AddedError()
        {
            string text = "A233";
            string expectedText = "A233 is not a valid road\r\n";

            _display.AddError(text);

            var content = _display.Message.ToString();

            Assert.Equal(expectedText, content);
        }

        [Fact]
        public void When_AddHelp_Is_Executed_Returns_AddHelp()
        {                       
            string expectedText = "Please enter:\r\n\tRoadStatus <RoadName>\r\n";
            string beforeCleanText = "text before clean";
            _display.Message = new StringBuilder(beforeCleanText);

            _display.AddHelp();

            var content = _display.Message.ToString();

            Assert.Equal(expectedText, content);           

        }

    }
}
