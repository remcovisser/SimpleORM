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


        // bla
        private MySqlModel<T> logicalOperatorBuilder(string logicalOperator, string field, string comparisonOpperator, object value)
        {
            query += " " + logicalOperator.ToUpper() + " " + field + " " + comparisonOpperator + " " + "'" + value + "'";

            return this;
        }


        // ----------------------------------- Sql query builders -------------------------------- //

        // Find data bij id
        public MySqlModel<T> find<U>(U value)
        {
            query += " WHERE id " + value;

            return this;
        }

        // Where builder
        public MySqlModel<T> where(string field, string comparisonOpperator, object value)
        {
            return logicalOperatorBuilder("where", field, comparisonOpperator, value);
        }

        // And builder
        public MySqlModel<T> and(string field, string comparisonOpperator, object value)
        {
            return logicalOperatorBuilder("and", field, comparisonOpperator, value);
        }

        // Or builder
        public MySqlModel<T> or(string field, string comparisonOpperator, object value)
        {
            return logicalOperatorBuilder("or", field, comparisonOpperator, value);
        }
    }
}
