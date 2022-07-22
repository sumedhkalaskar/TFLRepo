using System;
using System.Collections.Generic;
using System.Text;

namespace TFLRoadStatus.BL.Interface
{
   public interface IDisplay
    {
        Dictionary<string, string> ReportConstants { get; }
        StringBuilder Message { get; set; }
        IDisplay AddHeader(string text);
        IDisplay AddRoadStatusText(string key, string text);
        IDisplay AddError(string text);
        IDisplay AddHelp();
        void PrintStatus();
    }
}
