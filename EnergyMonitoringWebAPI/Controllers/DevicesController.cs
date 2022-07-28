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
    public class DevicesController : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        // GET: api/Devices
        public IEnumerable<Device> GetDevice()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Devices
                    //.Include(x => x.Sensors.Select(y => y.Unit))
                    .Include(x => x.Equipment)
                   .ToList();

                return items;
            }
        }

        // GET: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> GetDevice(int id)
        {
            Device device = await db.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        //GET: api/Devices/count
        [Route("api/devices/count")]
        public int GetDevicesCount()
        {
            int count = db.Devices.Where(x => (bool)x.Active).Count();

            return count;
        }

        // PUT: api/Devices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDevice(int id, Device device)
        {

            Equipment e = db.Equipments.Find(device.EquipmentID);

            device.Equipment = e;
            device.UpdateDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.DeviceID)
            {
                return BadRequest();
            }

            db.Entry(device).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
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

        // POST: api/Devices
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> PostDevice(Device device)
        {
            device.CreateDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Devices.Add(device);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = device.DeviceID }, device);
        }

        // DELETE: api/Devices/5
        [ResponseType(typeof(Device))]
        public async Task<IHttpActionResult> DeleteDevice(int id)
        {
            Device device = await db.Devices.FindAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            db.Devices.Remove(device);
            await db.SaveChangesAsync();

            return Ok(device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeviceExists(int id)
        {
            return db.Devices.Count(e => e.DeviceID == id) > 0;
        }
    }
}