using System;
using System.Collections.Generic;
using System.Reflection;
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
        string table;

        public MySqlModel()
        {
            baseModel = new Model<T>();
            connection = baseModel.MySqlconnection;
            table = this.GetType().Name.ToLower();
        }

        // Formate the data
        protected List<Tuple<int, string, FieldInfo>> formatData(MySqlDataReader data)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            int fieldCount = data.FieldCount;
            int x = 0;
            List<Tuple<int, string, FieldInfo>> formatedData = new List<Tuple<int, string, FieldInfo>>();

            while (data.Read())
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    if (i % fieldCount == 0)
                    {
                        x++;
                    }
                    formatedData.Add(new Tuple<int, string, FieldInfo>(x, data.GetString(i), fields[i]));
                }
            }
            data.Close();

            return formatedData;
        }

        // Return 1 instance of the chosen collection
        public T find(int id)
        {
            MySqlCommand query = new MySqlCommand("SELECT * FROM " + table + " WHERE id = " + id, connection);

            return baseModel.createInstaces(typeof(T), this.formatData(query.ExecuteReader())).First();
        }

        // Return all the data from the chosen collection
        public List<T> all()
        {  
            MySqlCommand query = new MySqlCommand("SELECT * FROM " + table, connection);
          
            return baseModel.createInstaces(typeof(T), this.formatData(query.ExecuteReader()));
        }
    }
}
