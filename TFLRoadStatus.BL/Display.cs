using System;
using System.Collections.Generic;
using System.Text;
using TFLRoadStatus.BL.Interface;

namespace TFLRoadStatus.BL
{
    public class Display :  IDisplay
    {
        public Dictionary<string, string> ReportConstants { get; } = new Dictionary<string, string>()
        {
            { "statusSeverityDescription", "Road Status Description" },
            { "statusSeverity", "Road Status" }
        };

        public StringBuilder Message { get; set; } = new StringBuilder();

        public IDisplay AddError(string text)
        {
            Message.AppendLine($"{text} is not a valid road");

            return this;
        }

        public IDisplay AddHeader(string text)
        {
            Message.AppendLine($"The status of the {text} is as follows");

            return this;
        }

        public IDisplay AddHelp()
        {
            Message.Clear();

            Message.AppendLine("Please enter:");
            Message.AppendLine("\tRoadStatus <RoadName>");

            return this;
        }

        public IDisplay AddRoadStatusText(string key, string text)
        {
            Message.AppendLine($"\t{ReportConstants[key]} is {text}");

            return this;
        }

        public void PrintStatus()
        {
            Console.WriteLine(Message.ToString());
        }
    }
}
