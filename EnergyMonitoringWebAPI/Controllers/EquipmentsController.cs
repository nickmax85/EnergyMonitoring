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
    public class EquipmentsController : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        // GET: api/Equipments
        public IEnumerable<Equipment> GetEquipment()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                return db.Equipments.ToList();
            }
        }

        // GET: api/Groups/5
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> GetEquipment(int id)
        {

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                Equipment equipment = await db.Equipments.FindAsync(id);
                if (equipment == null)
                {
                    return NotFound();
                }

                return Ok(equipment);

            }

        }

        // GET: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        [Route("api/equipments/number/{equipmentnumber}")]
        public async Task<IHttpActionResult> GetEquipmentByNumber(string equipmentnumber)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                //var sql = "select * from equipment where number like " + equipmentnumber;
                //var item = await db.Database.SqlQuery<Equipment>(sql).FirstOrDefaultAsync();

                var item = await db.Equipments.Where(x => x.Number.Equals(equipmentnumber))
                    .Include(x => x.Group)
                    .Include(x => x.Devices)
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);

            }
        }


        //GET: api/equipments/count
        [Route("api/equipments/count")]
        public int GetEquipmentsCount()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                //int count = db.Equipments.Count();

                var sql = "SELECT COUNT(*) FROM Equipment where Active = 1";
                var item = db.Database.SqlQuery<int>(sql).Single();

                return item;

            }


        }

        //GET: api/groups/2/equipments
        [Route("api/groups/{GroupId}/equipments")]
        public IEnumerable<Equipment> GetEquipmentsByGroup(int GroupId)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Equipments.Where(x => x.GroupID == GroupId)
                     .Include(x => x.Group)
                     .Include(x => x.Devices.Select(d => d.Sensors))
                     .ToList();

                return items;

            }

        }

        // GET: api/equipments/count/{year}/{weekday}
        [Route("api/equipments/count/{year}/{weekday}")]
        public IEnumerable<object> GetEquipmentsCountPerWeekday(int year, int weekday)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                var item = db.SpGetEquipmentsCount(year, weekday).ToList();

                return item;

            }

        }

        // PUT: api/Equipments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEquipment(int id, Equipment equipment)
        {
            equipment.UpdateDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipment.EquipmentID)
            {
                return BadRequest();
            }

            db.Entry(equipment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
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


        // POST: api/Equipments
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> PostEquipment(Equipment equipment)
        {

            if (equipment.CreateDate == null)
                equipment.CreateDate = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipments.Add(equipment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = equipment.EquipmentID }, equipment);
        }

        // DELETE: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> DeleteEquipment(int id)
        {
            Equipment equipment = await db.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipments.Remove(equipment);
            await db.SaveChangesAsync();

            return Ok(equipment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(int id)
        {
            return db.Equipments.Count(e => e.EquipmentID == id) > 0;
        }
    }
}