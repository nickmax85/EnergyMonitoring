using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EnergyMonitoringService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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


            InitData();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(1000, stoppingToken);
                return;
            }
        }

        private async void InitData()
        {
            // access database context with EF
            using (var context = new EnergyMonitoringContext())
            {
                // read devices with relation entities from database
                var devices = context.Device
               .Include(device => device.Sensor)
               .ThenInclude(sensor => sensor.Unit)
               .Include(device => device.Equipment)
               .Include(device => device.Equipment.Location)
               .ToList();


                // iterate over device list
                foreach (var device in devices)
                {
                    Console.WriteLine($"Device: ip={device.Ip}; name={device.Name}");
                    Console.WriteLine($"Location: name={device.Equipment.Location.Name}");
                    Console.WriteLine($"Equipment: number={device.Equipment.Number}; name={device.Equipment.Name}");

                    // read iostate from device
                    var url = "http://" + device.Ip + "/rest/json/iostate";
                    HttpClient request = new HttpClient();
                    var json = await request.GetStringAsync(url);

                    WebIOState deserialized = JsonConvert.DeserializeObject<WebIOState>(json);
                    Console.WriteLine(deserialized);

                    // iterate over sensor list
                    foreach (var sensor in device.Sensor)
                    {
                        if (sensor.Unit.Name.Equals("Pressure"))
                        {
                            Console.WriteLine($"Unit: name={sensor.Unit.Name}; sign={sensor.Unit.Sign}");
                            Console.WriteLine($"Sensor: id={sensor.SensorId};");
                        }
                    }

                }
            }
        }



        public class Input
        {
            public string name { get; set; }
            public int number { get; set; }
            public string unit { get; set; }
            public double value { get; set; }
        }

        public class Iostate
        {
            public List<Input> input { get; set; }
        }

        public class RootObject
        {
            public Iostate iostate { get; set; }
        }

    }
}
