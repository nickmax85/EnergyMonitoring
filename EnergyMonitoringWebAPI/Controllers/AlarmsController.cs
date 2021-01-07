using EnergyMonitoringWebAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class AlarmsController : ApiController

    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        // GET: api/Alarms
        public IEnumerable<Alarm> Get()
        {
           
          

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Alarms
                 .Include(x => x.Record.Sensor.Unit)
                 .Include(x => x.Record.Equipment.Group)
                 .ToList().Take(10);
          
                return items;
            }
        }

        // GET: api/alarms/2
        [Route("api/alarms/filter/{startDate}/{endDate}")]
        public IEnumerable<object> GetFilterAlarms(DateTime startDate, DateTime endDate)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.spGetFilterAlarms(startDate, endDate).ToList();

                return items;
            }

        }




        private List<string> GetSensorsFromDB()
        {

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                var items = (from s in db.Sensors
                             join d in db.Devices
                                 on s.DeviceID equals d.DeviceID
                             join u in db.Units
                                 on s.UnitID equals u.UnitID
                             where d.Active == true
                             select new
                             {
                                 Sensor = s,
                                 Device = d,

                             }).ToList();


                List<string> result = new List<string>();
                foreach (var item in items)
                {
                    result.Add(String.Format("Sensor={0}, Device={1}", item.Sensor, item.Device.Name));
                }


                return result;
            }
        }

       

        // GET: api/Alarms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Alarms
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Alarms/5
        [HttpPut()]
        public IHttpActionResult Put(int id, Alarm value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var alarm = db.Alarms.Find(id);

                alarm.Remark = value.Remark;
                alarm.Confirmed = value.Confirmed;
                alarm.UpdateDate = DateTime.Now;

                db.SaveChanges();

                ret = Ok(alarm);


            }

            return ret;
        }

        // DELETE: api/Alarms/5
        public void Delete(int id)
        {
        }
    }
}
