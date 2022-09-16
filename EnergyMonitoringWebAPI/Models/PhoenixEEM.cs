using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnergyMonitoringWebAPI.Models
{
    public class PhoenixEEM
    {
        public string context { get; set; }
        public string timestamp { get; set; }
        public List<Item> items { get; set; }
    }

    public class Item
    {
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public double value { get; set; }
        public string unit { get; set; }
        public string description { get; set; }

    }


}