using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using SimpleORM.Models;


namespace SimpleORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Users user = new Users().first().grab();
            Console.WriteLine(user.fullname());

            int usersSumTest = user.where("firstName", "=", "remco").sum("id");
            Console.WriteLine(usersSumTest);



            /* TODO
             * 
             *  - Interface voor mysql model maken die implementeren, zodat de nosql driver die kan gebruiken
             *  - Error handeling
             *  - Documentatie voor de gebruiker
             *  - Comments in code
             *  - Een nosql driver maken
             *  - Testen snelheid veel data
             */ 

            Console.ReadLine();
        }
    }
}
