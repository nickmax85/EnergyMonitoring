using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

                int oneMinute = 1000 * 60;

                int recordInterval = GetRecordIntervall();

                await Task.Delay(oneMinute * recordInterval, stoppingToken);

            }
         
        }



        private void GenerateTestdata()
        {
            using (var context = new EnergyMonitoringContext())
            {

                for (int i = 0; i < 1000; i++)
                {
                    Random r = new Random();
                    int rInt = r.Next(0, 100); //for ints
                    int range = 100;
                    double rDouble = r.NextDouble() * range; //for doubles


                    Record record = new Record();
                    record.EquipmentId = 2;
                    record.SensorId = 1;


                    record.Value = (decimal)Math.Round(rDouble, 1);
                    record.CreateDate = DateTime.Now;

                    //context.Record.Add(record);
                    //context.SaveChanges();
                    log.Info(record.Value);

                }



            }


        }


        private int GetRecordIntervall()
        {
            using (var context = new EnergyMonitoringContext())
            {

                var items = context.Config.ToList();

                var item = items.FirstOrDefault().RecordInterval;
                log.Info($"RecordInterval={item};");

                return item;
            }


        }
        private async void InitData()
        {
            // access database context with EF
            using (var context = new EnergyMonitoringContext())
            {
                // read devices with relation entities from database
                var devices = context.Device.Include(device => device.Sensor)
               .ThenInclude(sensor => sensor.Unit)
               .Include(device => device.Equipment)
               .Where(x => (bool)x.Active)
               .ToList();

                // iterate over device list
                foreach (var device in devices)
                {
                    try
                    {
                        // read json
                        var url = "http://" + device.Ip + "/rest/json";

                        HttpClient request = new HttpClient();
                        var json = await request.GetStringAsync(url);

                        WebIO obj = JsonConvert.DeserializeObject<WebIO>(json);
                        log.Info(device.Name);
                        // iterate over sensor list                
                        foreach (var sensor in device.Sensor)
                        {
                            // iterate over inputs
                            foreach (var item in obj.iostate.input)
                            {
                                if (sensor.Unit.Name.ToLower().Equals(item.name.ToLower()))
                                {
                                    StringBuilder sb = new StringBuilder();

                                    sb.Append(Environment.NewLine);
                                    sb.Append($"Equipment: number={device.Equipment.Number}; name={device.Equipment.Name}; ");
                                    sb.Append(Environment.NewLine);
                                    sb.Append($"Device: ip={device.Ip}; name={device.Name}; ");
                                    sb.Append(Environment.NewLine);
                                    sb.Append($"Sensor: id={sensor.SensorId}; ");
                                    sb.Append(Environment.NewLine);
                                    sb.Append($"Unit: name={sensor.Unit.Name}; sign={sensor.Unit.Sign}; ");
                                    sb.Append($"Input: {item.value}; ");
                                    sb.Append(Environment.NewLine);

                                    log.Info(sb);

                                    Record record = new Record();
                                    record.Equipment = device.Equipment;
                                    record.Sensor = sensor;
                                    record.Value = (decimal)Math.Round(item.value, 1);
                                    record.CreateDate = DateTime.Now;

                                    context.Record.Add(record);

                                    await context.SaveChangesAsync();

                                    CheckAlarm(record);

                                }
                            }
                        }

                    }
                    catch (JsonSerializationException ex)
                    {

                        log.Error(ex.StackTrace);

                    }

                    catch (HttpRequestException ex)
                    {

                        log.Error(ex.StackTrace);

                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.StackTrace);

                    }
                }
            }
        }

        private async void CheckAlarm(Record record)
        {
            using (var context = new EnergyMonitoringContext())
            {
                var config = context.Config.FirstOrDefault();

                DateTime dt = DateTime.Now;

                if ((int)dt.DayOfWeek == config.AuditDayOfWeek)
                {
                    if (dt.TimeOfDay >= config.AuditTimeStart && dt.TimeOfDay <= config.AuditTimeEnd)
                    {
                        if (record.Value < record.Sensor.LowerLimit || record.Value > record.Sensor.UpperLimit)
                        {
                            Alarm alarm = new Alarm();
                            alarm.RecordId = record.RecordId;
                            alarm.CreateDate = DateTime.Now;

                            log.Info($"Alarm: {alarm.AlarmId};");
                            context.Alarm.Add(alarm);

                            await context.SaveChangesAsync();
                        }

                    }
                }


            }
        }

    }
}
