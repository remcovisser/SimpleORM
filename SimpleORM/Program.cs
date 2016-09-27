using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace SimpleORM
{
    class Program
    {
        static void Main(string[] args)
        {
            Models.Users user = new Models.Users();
            user.firstName = "Karel";
            user.lastName = "de Groot";
            user.save();

           

            Console.ReadLine();
        }
    }
}
