using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnergyMonitoring.Models;

namespace EnergyMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly EnergyMonitoringContext _context;

        public EquipmentsController(EnergyMonitoringContext context)
        {
            _context = context;
        }

        // GET: api/Equipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
            using (var db = new EnergyMonitoringContext())
            {
                return await db.Equipment.ToListAsync();
            }

           
        }

        //GET: api/Default/5
        [HttpGet("api/groups/{groupId}/equipments")]
        public IEnumerable<Equipment> GetEquipmentsByGroup(int GroupId)
        {
            List<Equipment> items;

            using (var db = new EnergyMonitoringContext())
            {
                items = db.Equipment.Where(x => x.GroupId == GroupId)
                    .Include(x => x.Group)
                    .Include(x => x.Device.Select(d => d.Sensor))
                    .ToList();

                return items;
            }

        }

        // GET: api/Equipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        // PUT: api/Equipments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        {
            if (id != equipment.EquipmentId)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Equipments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipment", new { id = equipment.EquipmentId }, equipment);
        }

        // DELETE: api/Equipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipment>> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            return equipment;
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.EquipmentId == id);
        }
    }
}
