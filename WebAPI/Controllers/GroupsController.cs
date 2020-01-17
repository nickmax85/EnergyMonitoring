using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class GroupsController : ApiController
    {
        // GET: api/Location
        public IEnumerable<Group> Get()
        {
            List<Group> items;

            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                items = context.Group.Include(x => x.Equipment).OrderBy(x => x.Name).ToList();

            }
            return items;
        }

        // GET: api/Location/5
        public Group Get(int id)
        {
            Group Group;

            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                Group = context.Group.Where(x => x.GroupID == id).FirstOrDefault();

            }
            return Group;
        }


        


        [HttpPost]
        public IEnumerable<Group> Post([FromBody] Group Group)
        {
            using (EnergyMonitoringEntities context = new EnergyMonitoringEntities())
            {
                // context.Database.Log = Console.WriteLine;
                context.Configuration.ProxyCreationEnabled = false;
                if (Group.GroupID == 0)
                {
                    Group.CreateDate = System.DateTime.Now;
                    context.Group.Add(Group);
                }
                else
                {
                    Group.UpdateDate = System.DateTime.Now;
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
