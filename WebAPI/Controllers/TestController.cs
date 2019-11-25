using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class TestController : ApiController
    {

        static List<Device> devices = new List<Device>();

        //  private EnergyMonitoringEntities context = new EnergyMonitoringEntities();

        // GET: api/Default
        public IEnumerable<Device> Get()
        {
            var items = GetFromDB();

            // var items = context.Device.ToList();

            return items;
        }

        // GET: api/Default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Default
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Default/5
        public void Delete(int id)
        {
        }


        private List<Device> GetFromDB()
        {
            if (devices.Count == 0)
            {
                devices.Add(new Device() { DeviceID = 1, Name = "WEB IO 1" });
                devices.Add(new Device() { DeviceID = 2, Name = "WEB IO 2" });

            }
            return devices;
        }
    }
}
