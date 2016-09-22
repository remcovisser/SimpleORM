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

        public MySqlModel()
        {
            baseModel = new Model<T>();
            connection = baseModel.MySqlconnection;
            table = this.GetType().Name.ToLower();
            query = "SELECT * FROM " + table;
        }


        // Format the data
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


        // Execute the query
        public List<T> get()
        {
            MySqlCommand command = new MySqlCommand(query, connection);

            return baseModel.createInstaces(typeof(T), this.formatData(command.ExecuteReader()));
        }


        // Logical operator builder
        private MySqlModel<T> logicalOperatorBuilder(string logicalOperator, string field, string comparisonOperator, object value)
        {
            if (value != null)
            { 
                query += " " + logicalOperator.ToUpper() + " " + field + " " + comparisonOperator + " " + "'" + value + "'";
            } else
            {
                query += " " + logicalOperator.ToUpper() + " " + field + " " + comparisonOperator;
            }

            return this;
        }


        // ----------------------------------- Sql select builders -------------------------------- //

        // Find data bij id
        public MySqlModel<T> find<U>(U value)
        {
            query += " WHERE id " + value;

            return this;
        }

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
    }
}
