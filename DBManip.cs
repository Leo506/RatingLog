using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RatingLog.Database
{
    public class DBManip
    {
        private static MySqlConnection connection;
        private const string connectionString = "server=127.0.0.1;uid=root;password=26041986;database=ratinglog";

        static DBManip()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
        }

        ~DBManip()
        {
            connection.Close();
            connection.Dispose();
        }

        public static bool HasUser(string login, string password)
        {
            string command = $"call GetUser(\"{login}\", \"{password}\");";
            MySqlCommand cmd = new MySqlCommand(command, connection);

            var reader = cmd.ExecuteReader();

            var result = reader.HasRows;

            reader.Close();
            reader.Dispose();

            return result;
        }


        public static string[] GetAllGroups()
        {
            string command = "call GetAllGroupsName();";
            MySqlCommand cmd = new MySqlCommand(command, connection);

            var reader = cmd.ExecuteReader();
            List<string> groups = new List<string>();

            while (reader.Read())
                groups.Add(reader.GetString("groupname"));

            reader.Close();
            reader.Dispose ();

            return groups.ToArray();
        }

        public static DateTime[] GetAllDates(string groupName)
        {
            string command = $"call GetAllDatesByGroup(\"{groupName}\");";
            MySqlCommand cmd = new MySqlCommand(command, connection);

            var reader = cmd.ExecuteReader();
            List<DateTime> dates = new List<DateTime>();

            while (reader.Read())
            {
                var tmp = reader.GetString("date");
                dates.Add(DateTime.Parse(tmp));
            }

            reader.Close();
            reader.Dispose();

            Trace.WriteLine("Count of dates: " + dates.Count);

            return dates.ToArray();

        }

    }
}
