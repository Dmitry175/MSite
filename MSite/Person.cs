using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSite
{
    public class Person
    {
        public string Surname { get; set; }
        public string Forename { get; set; }
        public Title Title { get; set; }
    }

    public enum Title
    {
        Mr,
        Mrs,
        Miss
    }
}
