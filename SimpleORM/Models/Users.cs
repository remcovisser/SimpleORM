using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SimpleORM.Models
{
    class Users : ORM.MySqlModel<Users>
    {
        public int id;
        public string firstName;
        public string lastName;

        public string fullname()
        {
            return this.firstName + " " + this.lastName;
        }
    }
}
