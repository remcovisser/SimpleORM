using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SimpleORM.ORM
{
    class Model
    {
        public MySqlConnection MySqlconnection;
        public Model()
        {
            Database database = new Database();
            if (database.type == "mysql")
            {
                this.MySqlconnection = database.connect();
            }
        }
    }
}
