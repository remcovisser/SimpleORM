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
            List<Models.Users> remcos = users
                                        .groupby("firstName")
                                        
                                        .get();

            foreach(Models.Users remco in remcos)
            {
                Console.WriteLine(remco.fullname());
            }

            Console.ReadLine();
        }
    }
}
