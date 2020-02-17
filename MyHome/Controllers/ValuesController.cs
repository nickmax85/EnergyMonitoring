using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //fake read sessions from DB
            //var contentFile = System.Web.Hosting.HostingEnvironment.MapPath("~/data/sessions.json");
            //var content = File.ReadAllText(contentFile);

            //List<Session> sessions = JsonConvert.DeserializeObject<List<Session>>(content);
            //sessions.Add(new Session() { title = "Berndt Schulung", room = "C" });

            var json = JsonConvert.SerializeObject(null);

            return null;
        }

        // GET: api/Values/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
