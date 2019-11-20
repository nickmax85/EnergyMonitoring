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

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var context = new EnergyMonitoringContext())
                {
                    var devices = context.Device
                   .Include(device => device.Sensor)
                   .Include(device => device.Equipment)
                   .Include(device => device.Equipment.Location)
                   .ToList();

                    foreach (var item in devices)
                    {
                        Console.WriteLine($"Device: ip={item.Ip}; name={item.Name}");
                        Console.WriteLine($"Location: name={item.Equipment.Location.Name}");
                        Console.WriteLine($"Equipment: number={item.Equipment.Number}; name={item.Equipment.Name}");
                        foreach (var sensor in item.Sensor)
                        {
                            Console.WriteLine($"Sensor: id={sensor.SensorId}");
                        }

                    }
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
