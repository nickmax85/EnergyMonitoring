using EnergyMonitoringWebAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


namespace EnergyMonitoringWebAPI.Controllers
{
    public class Sensors1Controller : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        //// GET: api/Sensors
        //public IQueryable<Sensor> GetSensor()
        //{
        //    return db.Sensors.Where(x => (bool)x.Device.Active)
        //        .Include(x => x.Unit)
        //        .Include(x => x.Device.Equipment.Group);

        // GET: api/Sensors
        public IEnumerable<object> Get()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.GetSensors();

                return items;

            }
        }



        //// GET: api/Sensors
        //public IEnumerable<Sensor> GetSensor()
        //{


        //    var items = (from s in db.Sensors

        //                 join u in db.Units
        //                    on s.UnitID equals u.UnitID

        //                 join d in db.Devices
        //                     on s.DeviceID equals d.DeviceID

        //                 join e in db.Equipments
        //                     on d.EquipmentID equals e.EquipmentID

        //                 join g in db.Groups
        //                     on e.GroupID equals g.GroupID

        //                 where d.Active == true

        //                 select new
        //                 {
        //                     Sensor = s,
        //                     Unit = u,
        //                     Device = d,
        //                     Equipment = e,
        //                     Group = g
        //                 }).ToList();


        //    List<Sensor> result = new List<Sensor>();
        //    foreach (var item in items)
        //    {
        //        result.Add(item.Sensor);

        //    }
        //    return result;
        //}

        // GET: api/Sensors/5
        [ResponseType(typeof(Sensor))]
        public async Task<IHttpActionResult> GetSensor(int id)
        {
            Sensor sensor = await db.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(sensor);
        }
        //GET: api/sensors/count
        //[Route("api/sensors/count")]
        //public int GetSensorsCount()
        //{
        //    int count = db.Sensors.Include(x => x.Device)
        //        .Where(x => (bool)x.Device.Active)
        //        .Count();

        //    var sql = "SELECT COUNT(*) FROM Equipment";
        //    var item = db.Database.SqlQuery<int>(sql).Single();

        //    return count;
        //}


        //GET: api/devices/2/sensors
        [Route("api/devices/{DeviceId}/sensors")]
        public IEnumerable<Sensor> GetSensorsFromDevice(int DeviceId)
        {
            var items = db.Sensors.Where(x => x.DeviceID == DeviceId)
                 .Include(x => x.Unit)
                 .ToList();
            return items;
        }


        // PUT: api/Sensors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSensor(int id, Sensor sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sensor.SensorID)
            {
                return BadRequest();
            }

            db.Entry(sensor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sensors
        [ResponseType(typeof(Sensor))]
        public async Task<IHttpActionResult> PostSensor(Sensor sensor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sensors.Add(sensor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SensorExists(sensor.SensorID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sensor.SensorID }, sensor);
        }

        // DELETE: api/Sensors/5
        [ResponseType(typeof(Sensor))]
        public async Task<IHttpActionResult> DeleteSensor(int id)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SensorExists(int id)
        {
            return db.Sensors.Count(e => e.SensorID == id) > 0;
        }
    }
}