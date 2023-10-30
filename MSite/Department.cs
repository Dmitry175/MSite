using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSite
{
    public class Department
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Person> Members { get; set; }
    }
}
