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
            List<Models.Users> allUsers = users.all();

            foreach(Models.Users user in allUsers)
            {
                Console.WriteLine(user.id + ": " + user.firstName + " " + user.lastName);
                Console.WriteLine(user.fullname());
            }

            Models.Users aUser = users.find(1);
            Console.WriteLine(aUser.fullname());
            Console.ReadLine();
        }
    }
}
