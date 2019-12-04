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
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                InitData();

                await Task.Delay(5000, stoppingToken);

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
               .Include(device => device.Equipment.Area).Where(x => (bool)x.Active)
               .ToList();


                // iterate over device list
                foreach (var device in devices)
                {
                    Console.WriteLine($"Location: name={device.Equipment.Area.Name}");
                    Console.WriteLine($"Equipment: number={device.Equipment.Number}; name={device.Equipment.Name}; ");
                    Console.WriteLine($"Device: ip={device.Ip}; name={device.Name}");



                    try
                    {
                        // read json
                        var url = "http://" + device.Ip + "/rest/json";

                        HttpClient request = new HttpClient();
                        var json = await request.GetStringAsync(url);

                        WebIO obj = JsonConvert.DeserializeObject<WebIO>(json);
                        // iterate over sensor list                
                        foreach (var sensor in device.Sensor)
                        {
                            // iterate over inputs
                            foreach (var item in obj.iostate.input)
                            {
                                if (sensor.Unit.Name.ToLower().Equals(item.name.ToLower()))
                                {
                                    Console.WriteLine($"Sensor: id={sensor.SensorId};");
                                    Console.WriteLine($"Unit: name={sensor.Unit.Name}; sign={sensor.Unit.Sign}");
                                    Console.WriteLine($"Value: id={item.value};");

                                    Record rec = new Record();
                                    rec.Sensor = sensor;
                                    rec.Value = (decimal)Math.Round(item.value, 1);
                                    rec.CreateDate = DateTime.Now;

                                    if (rec.RecordId == 0)
                                        context.Record.Add(rec);

                                    context.SaveChanges();
                                }
                            }

                        }
                    }
                    catch (JsonSerializationException)
                    {


                    }

                    catch (HttpRequestException)
                    {


                    }




                }
            }
        }


    }
}
