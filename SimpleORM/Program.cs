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
            Models.Users remco = new Models.Users().find(55).grab();
            Console.WriteLine(remco.fullname());
            Console.WriteLine("User Found ---------------------------");

            remco.age = 100;
            remco.update();
            Console.WriteLine("User Updated ---------------------------");

            Models.Users user = new Models.Users();
            user.firstName = "Karel";
            user.lastName = "de Groot";
            user.age = 21;
            user.save();
            Console.WriteLine("User Created ---------------------------");

            List<Models.Users> users = new Models.Users().get();
            foreach(Models.Users aUser in users)
            {
                Console.WriteLine(aUser.fullname());
            }
            Console.WriteLine("Users Listed ---------------------------");

            List<Models.Users> bbUsers = new Models.Users().where("firstName", "=", "Karel").get();
            bool testDelete = new Models.Users().find(57).grab().delete();
            foreach(Models.Users bbUser in bbUsers)
            {
                //bbUser.delete();
            }
            Console.WriteLine("Users Deleted ---------------------------");



            Console.ReadLine();
        }
    }
}
