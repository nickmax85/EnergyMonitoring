using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class SensorsController : ApiController
    {
        // GET: api/Sensors
        public IEnumerable<object> Get()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.GetSensors().ToList();


                return items;
            }
        }

        // GET: api/Sensors/5
        public Sensor Get(int id)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var item = db.Sensors.Find(id);


                return item;
            }
        }

        //GET: api/sensors/count
        [Route("api/sensors/count")]
        public int GetSensorsCount()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                //    int count = db.Sensors.Include(x => x.Device)
                //        .Where(x => (bool)x.Device.Active)
                //        .Count();

                var sql = "SELECT COUNT(*) FROM Sensor " +
                    "WHERE DeviceID IN " +
                    "(SELECT DeviceID " +
                    "FROM Device " +
                    "WHERE Active = 1)";
                ;
                var item = db.Database.SqlQuery<int>(sql).Single();

                return item;

            }
        }

        //GET: api/devices/2/sensors
        [Route("api/devices/{DeviceId}/sensors")]
        public IEnumerable<Sensor> GetSensorsFromDevice(int DeviceId)
        {


            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var items = db.Sensors.Where(x => x.DeviceID == DeviceId)
                 .Include(x => x.Unit)
                 .ToList();
                return items;

            }
        }

        // POST: api/Sensors
        public void Post([FromBody]string value)
        {

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {


            }


        }

        // PUT: api/Sensors/5
        [HttpPut()]
        public IHttpActionResult Put(int id, Sensor value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var sensor = db.Sensors.Find(id);

                sensor.LowerLimit = value.LowerLimit;
                sensor.UpperLimit = value.UpperLimit;
                sensor.DeviceID = value.DeviceID;
                sensor.UnitID = value.UnitID;

                db.SaveChanges();

                ret = Ok(sensor);


            }


            return ret;
        }

        // DELETE: api/Sensors/5
        public void Delete(int id)
        {
        }
    }
}
