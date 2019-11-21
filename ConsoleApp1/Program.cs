using System;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            getJSON();

            while (true) ;
        }


        private async static void getJSON()
        {
            var url = "http://10.187.136.15/rest/json/iostate";
            HttpClient request = new HttpClient();
            var json = await request.GetStringAsync(url);

            Console.WriteLine(json);
        }

    }
}
