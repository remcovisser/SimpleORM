using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace SimpleORM.ORM
{
    class MySqlModel<T> : Model<T>
    {
        MySqlConnection connection;
        Model<T> baseModel;

        public MySqlModel()
        {
            baseModel = new Model<T>();
            connection = baseModel.MySqlconnection;
        }

        // Return all the data from the chosen collection
        public List<T> all()
        {
            string table =  this.GetType().Name.ToLower();
            MySqlCommand query = new MySqlCommand("SELECT * FROM " + table, connection);
            MySqlDataReader data = query.ExecuteReader();

            return baseModel.all(data, typeof(T));
        }
    }
}
