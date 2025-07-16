using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class ActivitiesController : ApiController
    {  
        public IEnumerable<Activity> GetActivity()
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                return db.Activity.ToList();
            }
        }


        // GET: api/alarms/2
        [Route("api/activities/filter/{startDate}/{endDate}")]
        public IEnumerable<object> GetFilterActivities(DateTime startDate, DateTime endDate)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Activity.Include(x=> x.Equipment)
                    .Where(x => DbFunctions.TruncateTime(x.CreateDate) >= DbFunctions.TruncateTime(startDate)
                    && DbFunctions.TruncateTime(x.CreateDate) <= DbFunctions.TruncateTime(endDate))
                    .OrderByDescending(x=> x.Date).ToList();

                var items2 = db.Activity
                   .Where(x => DbFunctions.TruncateTime(x.CreateDate) >= DbFunctions.TruncateTime(startDate)
                   && DbFunctions.TruncateTime(x.CreateDate) <= DbFunctions.TruncateTime(endDate))
                   .OrderByDescending(x => x.Date).ToList();

                return items2;
            }

        }


        // GET: api/activities/2
        [HttpGet]
        [Route("api/activities/equipment/{equipmentId}")]
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

        // GET: api/activities/5
        [HttpGet]
        //[Route("api/activities/{id}")]
        public Activity Get(int id)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                Activity activity = db.Activity.Find(id);


                return activity;

            }

        }

        // POST: api/Activities   
        [HttpPost()]
        public IHttpActionResult Post([FromBody] Activity value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                System.Diagnostics.Debug.WriteLine(value);

                var item = new Activity();
                item.Date = value.Date;
                item.Comment = value.Comment;
                item.ModifiedBy = value.ModifiedBy;
                item.CreateDate = DateTime.Now;
                item.UpdateDate = DateTime.Now;
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

                item.Date = value.Date;
                item.Comment = value.Comment;
                item.ModifiedBy = value.ModifiedBy;
                item.CreateDate = value.CreateDate;
                item.UpdateDate = DateTime.Now;

                db.SaveChanges();

                ret = Ok(item);

            }

            return ret;
        }

     
        public void Delete (int id)
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

            //return ret;
        }
    }
}
