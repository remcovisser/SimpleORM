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
        string query;
        string selectedFields;

        public MySqlModel()
        {
            baseModel = new Model<T>();
            connection = baseModel.MySqlconnection;
            table = this.GetType().Name.ToLower();
            query = "SELECT * FROM " + table;
            selectedFields = null;
        }


        // Format the data
        protected List<Tuple<int, string, FieldInfo>> formatData(MySqlDataReader data)
        {
            List<FieldInfo> fields = baseModel.filterFields(typeof(T), selectedFields);
            int fieldCount = data.FieldCount -1;
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


        // ----------------------------------- Create builders ------------------------------------ //
        public bool save()
        {
            List<Tuple<string, object>> formatedData = baseModel.saveOrUpdate(this, table);
            query = "insert into " + table;
            string fields = "(";
            string data = "(";

            foreach(Tuple<string, object> item in formatedData)
            {
                fields += item.Item1 + ",";
                data += "'" + item.Item2 + "',";
            }
            fields = fields.Remove(fields.Length - 1);
            data = data.Remove(data.Length - 1);

            query += fields + ") values " + data + ")";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            return true;
        }

        public bool update()
        {
            List<Tuple<string, object>> formatedData = baseModel.saveOrUpdate(this, table);
            query = "update " + table + " set ";
            string fieldsAndData = "";

            foreach (Tuple<string, object> item in formatedData)
            {
                fieldsAndData += item.Item1 + "='" + item.Item2 + "',";
            }
            fieldsAndData = fieldsAndData.Remove(fieldsAndData.Length - 1);

            query += fieldsAndData + " where id = " + this.GetType().GetFields().First().GetValue(this);
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();

            return true;
        }

        // ----------------------------------- Sql select builders -------------------------------- //

        // Select builder
        public MySqlModel<T> select(string fields)
        {
            selectedFields = fields;
            string[] selectedFieldsList = selectedFields.Replace(" ", string.Empty).Split(',');

            query = "select ";
            foreach (string selectedField in selectedFieldsList)
            {
                query += " " + selectedField;
                if (!selectedField.Equals(selectedFieldsList.Last()))
                {
                    query += ",";
                }
            }
            query += " from " + table;

            return this;
        }

        // Find builder, Always search on the id
        public MySqlModel<T> find<U>(U value)
        {
            query += " where id = " + value;

            return this;
        }

        // Return a list of the instance(s)
        public List<T> get()
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            return baseModel.createInstaces(typeof(T), this.formatData(command.ExecuteReader()));
        }

        // Return the instance
        public T grab()
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            return baseModel.createInstaces(typeof(T), this.formatData(command.ExecuteReader())).First();
        }

        // Return the amount of results 
        public int count()
        {
            List<T> results = get();

            return results.Count;
        }

        // Return the sum of the selected field
        // TODO: how do you get the value of a property of a generic?
        public int sum()
        {
            List<T> results = get();
            int sum = 0;

            foreach(T result in results)
            {
                var test = typeof(T).GetProperty("id");
                Type t = result.GetType();

                PropertyInfo prop = t.GetProperty("id");

                object list = prop.GetValue(t);
            }

            return 0;
        }


        // Logical operator builder helper
        private MySqlModel<T> logicalOperatorBuilder(string logicalOperator, string field, string comparisonOperator, object value)
        {
            // Group By
            if (value == null && comparisonOperator == null)
            {
                query += " " + logicalOperator.ToUpper() + " " + "'" + field + "'";
            }
            // Order By
            else if (value == null)
            {
                query += " " + logicalOperator.ToUpper() + " " + field + " " + comparisonOperator;
            }
            // Where
            else
            {
                query += " " + logicalOperator.ToUpper() + " " + field + " " + comparisonOperator + " " + "'" + value + "'";
            }

            return this;
        }


        // ----------------------------------- Sql comparison operators -------------------------------- //

        // Where builder
        public MySqlModel<T> where(string field, string comparisonOperator, object value)
        {
            return logicalOperatorBuilder("where", field, comparisonOperator, value);
        }

        // And builder
        public MySqlModel<T> and(string field, string comparisonOperator, object value)
        {
            return logicalOperatorBuilder("and", field, comparisonOperator, value);
        }

        // Or builder
        public MySqlModel<T> or(string field, string comparisonOperator, object value)
        {
            return logicalOperatorBuilder("or", field, comparisonOperator, value);
        }

        // OrderBy builder
        public MySqlModel<T> orderby(string field, string sort)
        {
            return logicalOperatorBuilder("order by", field, sort, null);
        }

        // GroupBy builder
        public MySqlModel<T> groupby(string field)
        {
            return logicalOperatorBuilder("group by", field, null, null);
        }
    }
}
