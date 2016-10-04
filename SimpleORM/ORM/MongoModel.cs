using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

using SimpleORM.Interfaces;

namespace SimpleORM.ORM
{
    //class MongoModel<T> : Model<T>, IModel<T, MySqlModel<T>>
    class MongoModel<T> : Model<T>
    {
        Model<T> baseModel;
        IMongoClient client;
        IMongoDatabase database;
        string table;
        string query;
        string selectedFields;

        public MongoModel()
        {
            baseModel = new Model<T>();
            client = new MongoClient();
            database = client.GetDatabase("test");
            table = this.GetType().Name.ToLower();
            query = "SELECT * FROM " + table;
            selectedFields = null;



            // Test
            var document = new BsonDocument
            {
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "zipcode", "10075" },
                        { "building", "1480" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                }
            };

            var collection = database.GetCollection<BsonDocument>("restaurants");
            collection.InsertOneAsync(document);
   
            var filter = new BsonDocument();
            var result =  collection.Find(filter).ToListAsync();

        }
    }
}