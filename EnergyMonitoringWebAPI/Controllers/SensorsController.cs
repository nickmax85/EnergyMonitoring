using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class SensorsController : ApiController
    {
        public IEnumerable<Sensor> GetSensor()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Sensors.ToList();

                return items;
            }
        }


        // GET: api/Sensors
        [ResponseType(typeof(Sensor))]
        [Route("api/sensors/filter/{searchInput}")]
        public IEnumerable<object> GetFilterSensor(string searchInput)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                List<SpGetSensors_Result> items;

                if (searchInput.Equals("*"))
                    items = db.SpGetSensors().ToList();
                else
                    items = db.SpGetSensors()
                        .Where(x => x.Equipment.ToLower().Contains(searchInput.ToLower())
                        || x.Group.ToLower().Contains(searchInput.ToLower())
                        || x.Unit.ToLower().Contains(searchInput.ToLower()))
                        .ToList();

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
                db.Configuration.LazyLoadingEnabled = false;
                int count = db.Sensors.Include(x => x.Device)
                    .Where(x => (bool)x.Device.Active)
                    .Count();

                //var sql = "SELECT COUNT(*) FROM Sensor " +
                //    "WHERE DeviceID IN " +
                //    "(SELECT DeviceID " +
                //    "FROM Device)";
                //;
               // var item = db.Database.SqlQuery<int>(sql).Single();

                return count;

            }
        }

        //GET: api/devices/2/sensors
        [Route("api/devices/{DeviceId}/sensors")]
        public IEnumerable<Sensor> GetSensorsFromDevice(int DeviceId)
        {


            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Sensors.Where(x => x.DeviceID == DeviceId)
                 .Include(x => x.Unit)
                 .ToList();
                return items;

            }
        }

        // POST: api/Sensors
        public async Task<IHttpActionResult> PostSensor(Sensor sensor)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                sensor.CreateDate = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Sensors.Add(sensor);
                await db.SaveChangesAsync();
            }
            return CreatedAtRoute("DefaultApi", new { id = sensor.SensorID }, sensor);
        }

        // PUT: api/Sensors/5
        [HttpPut()]
        public IHttpActionResult Put(int id, Sensor value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var sensor = db.Sensors.Find(id);

                sensor.LowerLimit = value.LowerLimit;
                sensor.UpperLimit = value.UpperLimit;
                if (value.DeviceID != 0)
                    sensor.DeviceID = value.DeviceID;
                if (value.UnitID != 0)
                    sensor.UnitID = value.UnitID;

                sensor.UpdateDate = DateTime.Now;

                db.SaveChanges();

                ret = Ok(sensor);


            }


            return ret;
        }

        // DELETE: api/Groups/5
        [ResponseType(typeof(Sensor))]
        public async Task<IHttpActionResult> DeleteSensor(int id)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                Sensor sensor = await db.Sensors.FindAsync(id);
                if (sensor == null)
                {
                    return NotFound();
                }

                db.Sensors.Remove(sensor);
                await db.SaveChangesAsync();

                return Ok(sensor);
            }
        }
    }
}
