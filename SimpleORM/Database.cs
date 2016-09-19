using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SimpleORM
{
    class Database
    {
        protected static MySqlConnection connect()
        { 
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "root";
            builder.Database = "world";

            MySqlConnection connection = new MySqlConnection(builder.ToString());
            connection.Open();

            return connection;
        }
    }
}
