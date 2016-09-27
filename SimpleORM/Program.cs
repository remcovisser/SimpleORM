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
            Models.Users user = new Models.Users().find(55).grab();
            Console.WriteLine(user.school().name);

            List<Models.Books> userBooks = user.books();
            foreach(Models.Books userBook in userBooks)
            {
                Console.WriteLine(userBook.name);
            }


            /* TODO
             * 
             *  - Interface voor mysql model maken die implementeren, zodat de nosql driver die kan gebruiken
             *  - Error handeling
             *  - Documentatie voor de gebruiker
             *  - Comments in code
             *  - Een nosql driver maken
             *  - Testen snelheid veel data
             *  - Sum method afmaken
             *  - Joins/Relaties?
             */ 

            Console.ReadLine();
        }
    }
}
