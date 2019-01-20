using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MyCoreTwo.Models;

//[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace MyCoreTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                //logging.AddDebug();
                //logging.AddEventSourceLogger();
                //logging.ClearProviders();

                var config = new ColoredConsoleLoggerConfiguration
                {
                    LogLevel = LogLevel.Warning,
                    Color = ConsoleColor.Yellow
                };
                logging.AddProvider(new ColoredConsoleLoggerProvider(config));
            })
                .UseStartup<Startup>();
    }
}
