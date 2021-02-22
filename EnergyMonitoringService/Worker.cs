using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
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
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ILogger<Worker> _logger;

        private Config Config;

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

                // calculate ms in one second
                int oneSecond = 1000 * 1;

                // calculate ms in one minute
                int oneMinute = 1000 * 60;

                // record interval in minutes
                int recordInterval = 10;

                // wenn datenbankeintrag vorhanden diesen intervall nehmen
                if (Config != null)
                    recordInterval = Config.RecordInterval;

                //await Task.Delay(oneSecond * 10, stoppingToken);
                await Task.Delay(oneMinute * recordInterval, stoppingToken);

            }
        }

        private void InitData()
        {
            GetData();
            //GetDataParallel();
        }

        private async void GetData()
        {
            // access database context with EF
            using (var context = new EnergyMonitoringContext())
            {
                try
                {
                    var config = context.Config.ToList().FirstOrDefault();
                    Config = config;
                    _logger.LogInformation($"RecordInterval={config} Minuten;");

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

                            // ping device
                            Ping ping = new Ping();
                            PingReply reply = ping.Send(device.Ip, 1500);

                            if (reply.Status == IPStatus.Success)
                            {
                                HttpClient request = new HttpClient();
                                var json = await request.GetStringAsync(url);

                                WebIO obj = JsonConvert.DeserializeObject<WebIO>(json);
                                _logger.LogInformation(device.Name);
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

                                            _logger.LogInformation(sb.ToString());

                                            Record record = new Record();
                                            record.Equipment = device.Equipment;
                                            record.Sensor = sensor;
                                            record.UnitId = sensor.UnitId;
                                            record.Value = (decimal)Math.Round(item.value, 1);
                                            record.CreateDate = DateTime.Now;

                                            context.Record.Add(record);
                                            await context.SaveChangesAsync();

                                            if (device.Equipment.Active == true)
                                                CheckAlarm(record);
                                        }
                                    }
                                }


                            }
                            else
                            {
                                _logger.LogError($"Fehler Verbindung: Status={device.Ip}; {reply.Status} DeviceName={device.Name};");
                            }



                        }
                        catch (JsonSerializationException ex)
                        {
                            _logger.LogError(ex.StackTrace);
                        }

                        catch (HttpRequestException ex)
                        {
                            _logger.LogError(ex.StackTrace);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.StackTrace);
                        }
                    }
                }
                catch (Microsoft.Data.SqlClient.SqlException e)
                {
                    _logger.LogInformation(e.StackTrace);
                }
            }
        }

        private async void CheckAlarm(Record record)
        {
            using (var context = new EnergyMonitoringContext())
            {
                DateTime dt = DateTime.Now;

                if ((int)dt.DayOfWeek == Config.AuditDayOfWeek)
                {
                    if (dt.TimeOfDay >= Config.AuditTimeStart && dt.TimeOfDay <= Config.AuditTimeEnd)
                    {
                        if (record.Value < record.Sensor.LowerLimit || record.Value > record.Sensor.UpperLimit)
                        {
                            Alarm alarm = new Alarm();
                            alarm.RecordId = record.RecordId;
                            alarm.CreateDate = DateTime.Now;

                            _logger.LogInformation($"Alarm: {record.Equipment.Name};");

                            context.Alarm.Add(alarm);

                            await context.SaveChangesAsync();
                        }

                    }
                }


            }
        }

    }
}

