using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RatingLog
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

    }
}
