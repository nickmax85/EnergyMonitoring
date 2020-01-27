using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EnergyMonitoringWeb.Controllers
{
    public class RecordsController : ApiController
    {
        private EnergyMonitoringEntities db = new EnergyMonitoringEntities();

        // GET: api/Records
        public IQueryable<Record> GetRecord()
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                //var item = context.Record.Where(x => x.Sensor.DeviceID == 2).ToList().AsQueryable();
                var item = context.Record
                    .Include(x => x.Sensor.Unit).Where(x => x.Sensor.DeviceID == 1).ToList().AsQueryable();

                return item;
            }

        }

        // GET: api/Records/5
        public IEnumerable<Record> Get(int id)
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                var item = context.Record
                     .Where(x => x.Sensor.DeviceID == id)
                     .Include(x => x.Sensor.Device)
                    .Include(x => x.Sensor.Unit)
                    .ToList();

                return item;
            }
        }

        // GET: api/Records/5
        [Route("api/records/{deviceId}/{startDate}/{endDate}")]
        public IEnumerable<Record> GetByDate(int deviceId, DateTime startDate, DateTime endDate)
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                var item = context.Record
                     .Where(x => x.Sensor.DeviceID == deviceId)
                     .Include(x => x.Sensor.Device)
                    .Include(x => x.Sensor.Unit)
                    .Where(x => DbFunctions.TruncateTime(x.CreateDate) >= DbFunctions.TruncateTime(startDate)
                    && DbFunctions.TruncateTime(x.CreateDate) <= DbFunctions.TruncateTime(endDate))
                    .ToList();

                return item;
            }
        }

        // PUT: api/Records/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecord(int id, Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != record.RecordID)
            {
                return BadRequest();
            }

            db.Entry(record).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
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

        // POST: api/Records
        [ResponseType(typeof(Record))]
        public IHttpActionResult PostRecord(Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Record.Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.RecordID }, record);
        }

        // DELETE: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult DeleteRecord(int id)
        {
            Record record = db.Record.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            db.Record.Remove(record);
            db.SaveChanges();

            return Ok(record);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordExists(int id)
        {
            return db.Record.Count(e => e.RecordID == id) > 0;
        }
    }
}