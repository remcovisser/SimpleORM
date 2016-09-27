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
            Models.Users users = new Models.Users();
            int remcos = users
                                        .select("id")
                                        .sum();

          

            Console.ReadLine();
        }
    }
}
