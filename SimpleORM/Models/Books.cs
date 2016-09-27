using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM.Models
{
    class Books: ORM.MySqlModel<Books>
    {
        public int id;
        public string name;
        public int user_id;
    }
}
