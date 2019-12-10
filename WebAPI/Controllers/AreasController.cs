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
        public Area Get(int id)
        {
            Area area;

            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                area = context.Area.Where(x => x.AreaID == id).FirstOrDefault();

            }
            return area;
        }


        


        [HttpPost]
        public IEnumerable<Area> Post([FromBody] Area area)
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                if (area.AreaID == 0)
                {
                    area.CreateDate = System.DateTime.Now;
                    context.Area.Add(area);
                }
                else
                {
                    area.UpdateDate = System.DateTime.Now;
                }
                context.SaveChanges();
            }

            return Get();
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
