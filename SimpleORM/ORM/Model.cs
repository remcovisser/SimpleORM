using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SimpleORM.ORM
{
    class Model<T>
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

        // Todo, make this so that it can also work with othe database.
        // Remove the MySqlDataReader
        public List<T> all(MySqlDataReader data, Type modelType)
        {
            FieldInfo[] fields = modelType.GetFields();
            int fieldCount = data.FieldCount;
            T instance = (T)Activator.CreateInstance(modelType);
            List<T> results = new List<T>();


            while (data.Read())
            {
                for (int i = 0; i < fieldCount; i++)
                {
                    fields[i].SetValue(instance, Convert.ChangeType(data.GetString(i), fields[i].FieldType));

                    if (i == (fieldCount - 1))
                    {
                        results.Add(instance);
                        instance = (T)Activator.CreateInstance(modelType);
                    }
                }
            }

            return results;
        }
    }
}
