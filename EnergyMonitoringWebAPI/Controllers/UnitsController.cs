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
    public class UnitsController : ApiController
    {
        private EnergyMonitoringContext db = new EnergyMonitoringContext();

        // GET: api/Units
        public IQueryable<Unit> GetUnit()
        {
            return db.Unit;
        }

        // GET: api/Units/5
        [ResponseType(typeof(Unit))]
        public async Task<IHttpActionResult> GetUnit(int id)
        {
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            return Ok(unit);
        }

        // PUT: api/Units/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUnit(int id, Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != unit.UnitID)
            {
                return BadRequest();
            }

            db.Entry(unit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Units
        [ResponseType(typeof(Unit))]
        public async Task<IHttpActionResult> PostUnit(Unit unit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Unit.Add(unit);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = unit.UnitID }, unit);
        }

        // DELETE: api/Units/5
        [ResponseType(typeof(Unit))]
        public async Task<IHttpActionResult> DeleteUnit(int id)
        {
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            db.Unit.Remove(unit);
            await db.SaveChangesAsync();

            return Ok(unit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UnitExists(int id)
        {
            return db.Unit.Count(e => e.UnitID == id) > 0;
        }
    }
}