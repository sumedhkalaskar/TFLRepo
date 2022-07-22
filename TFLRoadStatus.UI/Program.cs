using Microsoft.Extensions.DependencyInjection;
using System;
using TFLRoadStatus.BL.Interface;
using System.Linq;
using Microsoft.Extensions.Hosting;
using System.Collections.Specialized;
using System.Configuration;

namespace TFLRoadStatus.UI
{
    class Program
    {
        static int Main(string[] args)
        {
            var host = DependancyContainer.CreateHostBuilder(args)
                .Build();

            var app = host
                .Services
                .GetService<IRoadStatusChecker>();
            

            string parameter;

            if ((parameter = ParseArgs(args, host)) != null)
            {
                return app.GetRoadCurrentStatus(parameter,GetConfigValues("base_url"), GetConfigValues("app_id"), GetConfigValues("app_key"));
            }

            return -1;
           
        }
        private static string ParseArgs(string[] args, IHost host)
        {
            if (args != null && args.Length == 1)
            {
                return args.First();
            }

            PrintHelp(host);

            return null;
        }

        private static void PrintHelp(IHost host)
        {
            var print = host.Services.GetService<IDisplay>();

            print.AddHelp().PrintStatus();
        }
        private static string GetConfigValues(string key)
        {
            var settings = (NameValueCollection)ConfigurationManager.GetSection("appSettings");

            return settings[key]?.ToString() ?? "";
            
        }
    }
}
