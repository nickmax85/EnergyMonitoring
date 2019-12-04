using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class AreasController : ApiController
    {
        // GET: api/Location
        public IEnumerable<Area> Get()
        {
            List<Area> items;

            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                items = context.Area.Include(x => x.Equipment).OrderBy(x => x.Name).ToList();

            }
            return items;
        }

        // GET: api/Location/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Location
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Location/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Location/5
        public void Delete(int id)
        {
        }
    }
}
