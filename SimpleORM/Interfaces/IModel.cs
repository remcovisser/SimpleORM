using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SimpleORM.ORM;

namespace SimpleORM.Interfaces
{
    interface IModel<O, T>
    {
        // Properties
        //Model<O> baseModel { get; set; }
        //string table { get; set; }
        //string query { get; set; }
        //string selectedFields { get; set; }

        // Methods
        bool save();
        bool update();
        bool delete();
        int count();
        int sum(string value);
        O grab();
        List<O> get();
        T select(string fields);
        T find<U>(U value);
        T first();
        T logicalOperatorBuilder(string logicalOperator, string field, string comparisonOperator, object value);
        T where(string field, string comparisonOperator, object value);
        T and(string field, string comparisonOperator, object value);
        T or(string field, string comparisonOperator, object value);
        T orderby(string field, string sort);
        T groupby(string field);
        O hasOne<U>(U parent, string customField = null);
        List<O> hasMany<U>(U parent, string customField = null);
        List<Tuple<int, string, FieldInfo>> formatData(MySqlDataReader data);
    }
}
