using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using Serilog;

namespace EnergyMonitoringService
{
    public class Program
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main(string[] args)
        {

            // Logging
            // https://stackify.com/making-log4net-net-core-work/
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Programm Start");

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureLogging(loggerFactory => loggerFactory.AddEventLog())
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    var serilogLogger = new LoggerConfiguration()
                        .WriteTo.RollingFile("EnergyMonitoringServiceLog.txt")
                        .CreateLogger();

                    services.AddLogging(builder =>
                    {
                        builder.SetMinimumLevel(LogLevel.Information);
                        builder.AddSerilog(logger: serilogLogger, dispose: true);
                    });


                });

    }
}
