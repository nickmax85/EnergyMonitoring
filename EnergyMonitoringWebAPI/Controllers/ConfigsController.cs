using Microsoft.Web.Infrastructure.DynamicValidationHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class ConfigsController : ApiController
    {
        // GET: api/Configs
        public IEnumerable<Config> Get()
        {

            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var items = db.Configs.ToList();

                return items;
            }
        }

        // GET: api/Configs/5
        public Config Get(int id)
        {
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var item = db.Configs.Find(id);


                return item;
            }
        }

        // POST: api/Configs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Configs/5
        [HttpPut()]
        public IHttpActionResult Put(int id, Config value)
        {
            IHttpActionResult ret = null;
            using (EnergyMonitoringContext db = new EnergyMonitoringContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var config = db.Configs.Find(id);

                config.RecordInterval = value.RecordInterval;
                config.AuditDayOfWeek = value.AuditDayOfWeek;
                config.AuditTimeStart = value.AuditTimeStart;
                config.AuditTimeEnd = value.AuditTimeEnd;
                config.AirPressurePrice = value.AirPressurePrice;
                config.UpdateDate = DateTime.Now;

                db.SaveChanges();

                ret = Ok(config);


            }


            return ret;



        }

        // DELETE: api/Configs/5
        public void Delete(int id)
        {
        }
    }
}
