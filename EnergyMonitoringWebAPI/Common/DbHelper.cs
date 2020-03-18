using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EnergyMonitoringWebAPI.Common
{
    public class DbHelper
    {

        static string _connectionString = @"Data Source=localhost;Initial Catalog=EnergyMonitoring;Integrated Security=True;Connect Timeout=30";
        //static string _connectionString = @"server=localhost;user=root;database=EnergyMonitoring;password=root;";

        public static List<Device> GetAlarms()
        {
            string sql = "select DeviceID, Name from device order by name";

            List<Device> result = new List<Device>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows && reader.Read())
                    {
                        Device item = new Device();
                        item.DeviceID = (int)reader["DeviceID"];
                        item.Name = reader["Name"] == DBNull.Value ? null : (string)reader["Name"];

                        result.Add(item);
                        Console.WriteLine(item.Name);
                    }
                }
            }
            return result;
        }

        public static List<Sensor> GetSensors()
        {

            List<Sensor> result = new List<Sensor>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GetSensors", conn);
                command.CommandType = CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows && reader.Read())
                    {
                        Sensor item = new Sensor();

                        item.Device = new Device();
                        item.Device.Name = (string)reader["Name"];
                        item.Device.IP = (string)reader["IP"];

                        item.Device.Equipment = new Equipment();
                        item.Device.Equipment.Number = (string)reader["Number"];
                        item.Device.Equipment.Name = (string)reader["Name"];



                    }
                }
            }
            return result;
        }

        public static List<Sensor> GetNativeSql()
        {
            string sql = "select Unit.Name as unit, Sensor.LowerLimit, Sensor.UpperLimit, Device.Name as device " +
                            "from Sensor " +
                                "join Unit " +
                                    "on Unit.UnitID = Sensor.UnitID " +
                                "join Device " +
                                    "on Device.DeviceID = Sensor.DeviceID " +
                                    "where Device.Active = 1"


                                    ;

            List<Sensor> result = new List<Sensor>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.HasRows && reader.Read())
                    {
                        Sensor item = new Sensor();

                        item.Unit = new Unit();
                        item.Unit.Name = reader["unit"] == DBNull.Value ? null : (string)reader["unit"];

                        item.Device = new Device();
                        item.Device.Name = reader["device"] == DBNull.Value ? null : (string)reader["device"];

                        result.Add(item);
                        Console.WriteLine(item);
                    }
                }
            }
            return result;
        }

        public static List<string> GetJson1()
        {

            List<string> result = new List<string>();

            dynamic flexible = new ExpandoObject();
            flexible.Int = 3;
            flexible.String = "hi";

            var dictionary = (IDictionary<string, object>)flexible;
            dictionary.Add("Bool", false);

            var serialized = JsonConvert.SerializeObject(dictionary); // {"Int":3,"String":"hi","Bool":false}

            result.Add(serialized);

            return result;
        }


        public static List<string> GetJson2()
        {

            List<string> result = new List<string>();

            var columns = new Dictionary<string, string>
            {
                { "FirstName", "Mathew"},
                { "Surname", "Thompson"},
                { "Gender", "Male"},
                { "SerializeMe", "GoOnThen"}
            };

            var jsSerializer = new JavaScriptSerializer();

            var serialized = jsSerializer.Serialize(columns);

            result.Add(serialized);

            return result;
        }


        //// GET: api/Sensors
        //public IEnumerable<Sensor> GetSensor()
        //{


        //    var items = (from s in db.Sensors

        //                 join u in db.Units
        //                    on s.UnitID equals u.UnitID

        //                 join d in db.Devices
        //                     on s.DeviceID equals d.DeviceID

        //                 join e in db.Equipments
        //                     on d.EquipmentID equals e.EquipmentID

        //                 join g in db.Groups
        //                     on e.GroupID equals g.GroupID

        //                 where d.Active == true

        //                 select new
        //                 {
        //                     Sensor = s,
        //                     Unit = u,
        //                     Device = d,
        //                     Equipment = e,
        //                     Group = g
        //                 }).ToList();


        //    List<Sensor> result = new List<Sensor>();
        //    foreach (var item in items)
        //    {
        //        result.Add(item.Sensor);

        //    }
        //    return result;
        //}
    }
}