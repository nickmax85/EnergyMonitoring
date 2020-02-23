using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.Models;

namespace Test.Controllers
{
    public class DataController : ApiController
    {

        static List<Data> data = new List<Data>();

        private string getDataString()
        {

            //fake read sessions from DB
            var contentFile = System.Web.Hosting.HostingEnvironment.MapPath("~/data/cmi.json");
            var content = File.ReadAllText(contentFile);

            List<Data> data = JsonConvert.DeserializeObject<List<Data>>(content);
            data.Add(new Data() { Number = 99, Value = 10.0f });

            var json = JsonConvert.SerializeObject(data);

            return json;

        }

        private List<Data> getDataObject()
        {

            //fake read sessions from DB
            var contentFile = System.Web.Hosting.HostingEnvironment.MapPath("~/data/data.json");
            var content = File.ReadAllText(contentFile);


            List<Data> data = JsonConvert.DeserializeObject<List<Data>>(content);
            //List<Data> data = new List<Data>();
            data.Add(new Data() { Number = 99, Value = 10.0f });

            return data;

        }

        // GET: api/Data
        public IEnumerable<Data> Get()
        {
            return getDataObject();
        }

        // GET: api/Data/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Data
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Data/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Data/5
        public void Delete(int id)
        {
        }
    }
}
