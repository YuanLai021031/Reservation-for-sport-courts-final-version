using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservation_for_sport_courts_final_version.service
{
    public class MySqlConnector
    {
        private readonly MySqlConnection _connection;
        public MySqlConnector()
        {
            string connectionString = "Server=mariadb.vamk.fi;DATABASE=e2000581_court_reservation;UID=e2000581;PASSWORD=aa69aRMFWmT";
            _connection = new MySqlConnection(connectionString);
        }


        public MySqlConnection getConnection()
        {
            return _connection;
        }

        public void openConnection()
        {
            _connection.Open();
        }

        public void closeConnection() { 
            _connection.Close(); 
        
        }

        public List<Dictionary<string, string>> executeQuery(
            string commandText, 
            List<string> fields,
            Dictionary<string, object> parameters = null)
        {
            using (MySqlCommand command = new MySqlCommand(commandText, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                using (var reader = command.ExecuteReader())
                {
                    return getFields(reader, fields);
                }
            }
        }

        public int executeNonQuery(
            string commandText,
            Dictionary<string, object> parameters = null)
        {
            using (MySqlCommand command = new MySqlCommand(commandText, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        private List<Dictionary<string, string>> getFields(MySqlDataReader reader, List<string> fields) {
            if (fields == null || fields.Count == 0)
            {
                return null;
            }
            var results = new List<Dictionary<string, string>>();
            while (reader.Read())
            {
                var row = new Dictionary<string, string>();
                foreach (var field in fields)
                {
                    row.Add(field, reader.GetString(field));
                }
                results.Add(row);
            }
            reader.Close();
            return results;
        }
    }
}
