using EnergyMonitoringWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnergyMonitoringWebAPI.Controllers
{
    public class PhoenixController : ApiController
    {

        static List<PhoenixEEM> trainings = new List<PhoenixEEM>();

        // GET: api/Phoenix
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Phoenix/5
        [Route("api/phoenix/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {

            var url = "http://10.187.36.3/api/v1/measurements";
            HttpClient request = new HttpClient();
            var json = await request.GetStringAsync(url);
            var item = JsonConvert.DeserializeObject<PhoenixEEM>(json);
            return Ok(item);
        }


        // POST: api/Phoenix
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Phoenix/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Phoenix/5
        public void Delete(int id)
        {
        }
    }
}
