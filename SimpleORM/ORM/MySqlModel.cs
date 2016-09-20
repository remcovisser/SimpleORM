using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace SimpleORM.ORM
{
    class MySqlModel<T> : Model
    {
        MySqlConnection connection;
        public MySqlModel()
        {
            Model model = new Model();
            connection = model.MySqlconnection;
        }

        // Return all the data from the chosen collection
        public List<T> all()
        {
            string table =  this.GetType().Name.ToLower();
            MySqlCommand query = new MySqlCommand("SELECT * FROM " + table, connection);
            MySqlDataReader data = query.ExecuteReader();

          
            // Test
            // Move this to the Model.cs
            var results = new List<T>();
            FieldInfo[] fields = this.GetType().GetFields();
            int count = data.FieldCount;
            T instance = (T)Activator.CreateInstance(this.GetType());


            while (data.Read())
            {
                for (int i = 0; i < count; i++)
                {
                    fields[i].SetValue(instance, Convert.ChangeType(data.GetString(i), fields[i].FieldType));

                    if(i == (data.FieldCount - 1))
                    {
                        results.Add(instance);
                        instance = (T)Activator.CreateInstance(this.GetType());
                    }
                }
            }
          
            return results;
        }
    }
}
