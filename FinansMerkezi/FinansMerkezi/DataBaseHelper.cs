using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace FinansMerkezi
{
    public class DataBaseHelper
    {
        public static MySqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            return new MySqlConnection(connectionString);
        }
    }
}
