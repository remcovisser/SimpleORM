using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM.Models
{
    class Schools : ORM.MySqlModel<Schools>
    {
        public int id;
        public string name;
    }
}
