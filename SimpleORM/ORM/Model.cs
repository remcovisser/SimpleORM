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


        // Filter out the fields that are not selected
        public List<FieldInfo> filterFields(Type modelType, string selectedFields)
        {
            FieldInfo[] fields = modelType.GetFields();
            List<FieldInfo> filteredFields = new List<FieldInfo>();

            if (selectedFields == null)
            {
                foreach (FieldInfo field in fields)
                {
                    filteredFields.Add(field); 
                }
            }
            else
            {
                foreach (FieldInfo field in fields)
                {
                    string[] selectedFieldsList = selectedFields.Replace(" ", string.Empty).Split(',');
                    if (selectedFieldsList.Contains(field.Name))
                    {
                        filteredFields.Add(field);
                    }
                }
            }

            return filteredFields;
        }

        // ----------------------------------- Sql select -------------------------------- //

        // Create a list of instace of type T
        public List<T> createInstaces(Type modelType, List<Tuple<int, string, FieldInfo>> formatedData)
        {
            T instance = (T)Activator.CreateInstance(modelType);
            List<T> results = new List<T>();
            int rowId = 1;
          
            foreach (Tuple<int, string, FieldInfo> row in formatedData)
            {
                // Temp fix for last item
                if(row.Equals(formatedData.Last()))
                {
                    row.Item3.SetValue(instance, Convert.ChangeType(row.Item2, row.Item3.FieldType));
                    results.Add(instance);
                    break;
                }

                if (row.Item1 != rowId)
                {
                    results.Add(instance);
                    instance = (T)Activator.CreateInstance(modelType);
                    rowId++;
                }

                row.Item3.SetValue(instance, Convert.ChangeType(row.Item2, row.Item3.FieldType));
            }

            return results;
        }

        // ----------------------------------- Sql insert -------------------------------- //
        public List<Tuple<string, object>> saveOrUpdate<T>(T instance, string table)
        {
            Type modeltype = instance.GetType();
            FieldInfo[] fields = modeltype.GetFields();
            List<Tuple<string, object>> formatedData = new List<Tuple<string, object>>();

            foreach(FieldInfo field in fields)
            {
                // Filter out the connection field, needs a fix
                if (field.Equals(fields.Last()))
                {
                    break;
                }

                // Skip id field
                if (!field.Equals(fields.First()))
                {
                    string fieldName = field.Name;
                    object value = field.GetValue(instance);
                    formatedData.Add(new Tuple<string, object>(field.Name, field.GetValue(instance)));
                }
            }

            return formatedData ;
        }
    }
}
