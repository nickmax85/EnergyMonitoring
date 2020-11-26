using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class ActivitiesController : ApiController
    {
        // GET: api/Activities
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Activities/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Activities
        public IHttpActionResult Post([FromBody] Activity value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                System.Diagnostics.Debug.WriteLine(value);

                var item = new Activity();
                item.Comment = value.Comment;
                item.CreateDate = System.DateTime.Now;
                item.EquipmentID = value.EquipmentID;

                db.Activity.Add(item);

                db.SaveChanges();

                ret = Ok(value);
            }
            return ret;
        }

        // PUT: api/Activities/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Activities/5
        public void Delete(int id)
        {
        }
    }
}
