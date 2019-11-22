using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class WebIO
    {

        public Info info { get; set; }
        public Iostate iostate { get; set; }
        public System system { get; set; }

        public class Info
        {
            public string request { get; set; }
            public string time { get; set; }
            public string ip { get; set; }
            public string devicename { get; set; }
        }

        public class Input
        {
            public string name { get; set; }
            public int number { get; set; }
            public string unit { get; set; }
            public double value { get; set; }
        }

        public class Iostate
        {
            public List<Input> input { get; set; }
        }

        public class Diagnosis
        {
            public string time { get; set; }
            public string msg { get; set; }
        }

        public class Diagarchive
        {
            public string time { get; set; }
            public string msg { get; set; }
        }

        public class System
        {
            public string time { get; set; }
            public List<Diagnosis> diagnosis { get; set; }
            public List<Diagarchive> diagarchive { get; set; }
        }
    }
}
