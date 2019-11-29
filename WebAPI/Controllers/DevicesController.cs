using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class DevicesController : ApiController
    {
        private List<Device> items;


        // GET: api/devices    
        public IEnumerable<Device> Get()
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                items = context.Device.
                    Include(x => x.Equipment.Location).Where(x => (bool)x.Active).ToList();

            }
            return items;
        }

        // GET: api/Default/5
        public IEnumerable<Device> Get(int id)
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                context.Configuration.ProxyCreationEnabled = false;

                return context.Device.Where(x => x.DeviceID == id).ToList();
            }

        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }



    }
}
