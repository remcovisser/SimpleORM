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
        string server, user, password, database;
        public string type;

        // Database settings
        public Database()
        {
            this.server = "localhost";
            this.user = "root";
            this.password = "root";
            this.database = "simpleorm";
            this.type = "mysql";
        }
       
        public MySqlConnection connect()
        { 
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = this.server;
            builder.UserID = this.user;
            builder.Password = this.password;
            builder.Database = this.database;

            MySqlConnection connection = new MySqlConnection(builder.ToString());
            connection.Open();

            return connection;
        }
    }
}
