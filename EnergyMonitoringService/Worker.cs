using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnergyMonitoringService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnergyMonitoringService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            using (var context = new EnergyMonitoringContext())
            {
                //var devices = _context.Device
                // .Include(device => device.Equipment)
                // .ToList();

                //foreach (var item in devices)
                //{
                //    Console.WriteLine(item.Ip);
                //    Console.WriteLine(item.Equipment.Name);
                //}



                var devices = context.Device
               .Include(device => device.Sensor)
               .ToList();

                foreach (var item in devices)
                {
                    Console.WriteLine(item.Ip);
                    foreach (var sensor in item.Sensor)
                    {
                        Console.WriteLine(sensor.SensorId);
                    }

                }
            }


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                //foreach (var item in items)
                //{
                //    Console.WriteLine(item.Ip);
                //    Console.WriteLine(item.Equipment.Name);

                //}





                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
