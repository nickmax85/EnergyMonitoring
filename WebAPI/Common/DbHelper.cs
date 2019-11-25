using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebAPI.Common
{
    public class DbHelper
    {

        static string _connectionString = @"Data Source=localhost;Initial Catalog=EnergyMonitoring;Integrated Security=True;Connect Timeout=30";
        //static string _connectionString = @"server=localhost;user=root;database=EnergyMonitoring;password=root;";

        public static List<Device> GetAllDevices()
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
    }
}