using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetJSON();
            FileRead();
            while (true) ;
        }


        private async static void GetJSON()
        {
            // full
            var url = "http://10.187.136.15/rest/json";
            HttpClient request = new HttpClient();
            var json = await request.GetStringAsync(url);

            WebIO obj = JsonConvert.DeserializeObject<WebIO>(json);
            Console.WriteLine(obj.iostate.input[0].name);

        }

        private static void FileRead()
        {
            var content = File.ReadAllText("data/webio.json");
            WebIO obj = JsonConvert.DeserializeObject<WebIO>(content);
            Console.WriteLine(obj.iostate.input[0].value);
            Console.WriteLine(Math.Round(obj.iostate.input[0].value, 2));

        }
    }




}
