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
            return new string[] { "value1", "value2" };
        }

        // GET: api/Configs/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Configs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Configs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Configs/5
        public void Delete(int id)
        {
        }
    }
}
