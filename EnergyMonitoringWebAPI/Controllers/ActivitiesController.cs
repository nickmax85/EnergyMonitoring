using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class ActivitiesController : ApiController
    {
        // GET: api/activities/2
        [Route("api/activities/{equipmentId}")]
        public IEnumerable<Activity> GetActivitiesByEquipment(int equipmentId)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Activity
                     .Where(x => x.EquipmentID == equipmentId)
                     .OrderByDescending(x => x.CreateDate).ToList();

                return items;
            }

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
                item.ModifiedBy = value.ModifiedBy;
                item.CreateDate = System.DateTime.Now;
                item.EquipmentID = value.EquipmentID;

                db.Activity.Add(item);

                db.SaveChanges();

                ret = Ok(value);
            }
            return ret;
        }

        // PUT: api/Sensors/5
        [HttpPut()]
        public IHttpActionResult Put(int id, Activity value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var item = db.Activity.Find(id);

                item.Comment = value.Comment;
                item.ModifiedBy = value.ModifiedBy;
                item.CreateDate = value.CreateDate;
                item.UpdateDate = DateTime.Now;

                db.SaveChanges();

                ret = Ok(item);

            }

            return ret;
        }

        // DELETE: api/Activities/5
        public void Delete(int id)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var item = db.Activity.Find(id);
                db.Activity.Remove(item);

                db.SaveChanges();

                ret = Ok(item);
            }
        }
    }
}
