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
using EnergyMonitoringWebAPI.Models;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class EquipmentsController : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        // GET: api/Equipments
        public IQueryable<Equipment> GetEquipment()
        {
            return db.Equipment;
        }

        // GET: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> GetEquipment(int id)
        {
            Equipment equipment = await db.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        //GET: api/groups/2/equipments
        [Route("api/groups/{GroupId}/equipments")]
        public IEnumerable<Equipment> GetEquipmentsByGroup(int GroupId)
        {
            var items = db.Equipment.Where(x => x.GroupID == GroupId)
                 .Include(x => x.Group)
                 .Include(x => x.Device.Select(d => d.Sensor))
                 .ToList();

            return items;
        }



        // PUT: api/Equipments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEquipment(int id, Equipment equipment)
        {
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipment.Add(equipment);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = equipment.EquipmentID }, equipment);
        }

        // DELETE: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public async Task<IHttpActionResult> DeleteEquipment(int id)
        {
            Equipment equipment = await db.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipment.Remove(equipment);
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
            return db.Equipment.Count(e => e.EquipmentID == id) > 0;
        }
    }
}