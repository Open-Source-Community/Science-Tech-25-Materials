using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC
{
    public class Director
    {
        public int id {  get; set; }

        public string name { get; set; }

        public List<Movie> movies { get; set; }

        public int nationalityId { get; set; }

        public Nationality nationality { get; set; }
    }
}
