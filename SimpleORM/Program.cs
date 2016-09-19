using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SimpleORM
{
    class Program : Database
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = Database.connect();
            MySqlCommand cities = new MySqlCommand("SELECT * FROM city", connection);
            MySqlDataReader citiesRDR = cities.ExecuteReader();

            while (citiesRDR.Read())
            {
                Console.WriteLine(citiesRDR.GetString(1));
            }


            Console.ReadLine();
        }
    }
}
