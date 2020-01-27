using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EnergyMonitoringWeb.Controllers
{
    public class EquipmentsController : ApiController
    {
        // GET: api/Equipments
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Equipments/5
        public string Get(int id)
        {
            return "value";
        }

        //GET: api/Default/5
        [Route("api/Groups/{GroupId}/equipments")]
        public IEnumerable<Equipment> GetEquipmentsByGroup(int GroupId)
        {
            List<Equipment> items;

            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;
                items = context.Equipment.Where(x => x.GroupID == GroupId)
                    .Include(x => x.Group)
                    .Include(x => x.Device.Select(d => d.Sensor))
                    .ToList();

                return items;
            }

        }

        // POST: api/Equipments
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Equipments/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Equipments/5
        public void Delete(int id)
        {
        }
    }
}
