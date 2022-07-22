using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net.Http;
using TFLRoadStatus.BL;
using TFLRoadStatus.BL.Interface;

namespace TFLRoadStatus.UI
{
    public static class DependancyContainer
    {
        
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());

                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient();
                    //services.AddSingleton<IConfig, Config>();
                    services.AddSingleton<IDisplay, Display>();
                    services.AddSingleton<IRestClient, RestClient>();
                    services.AddSingleton<IRoadStatusChecker, RoadStatusCheckercs>();

                });
                

            return hostBuilder;
        }
    }
}
