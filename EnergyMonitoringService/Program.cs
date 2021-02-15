using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;



namespace EnergyMonitoringService
{
    public class Program
    {  
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseWindowsService()
               .ConfigureLogging((hostingContext, config) =>
               {        
                   config.AddLog4Net("log4net.config", true);
                   config.SetMinimumLevel(LogLevel.Debug);
               })
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>();
               });

    }
}
