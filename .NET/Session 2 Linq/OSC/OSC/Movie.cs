using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSC
{
    public class Movie
    {
        public int id {  get; set; }

        public string title {  get; set; }

        public string description {  get; set; }

        public Director director {  get; set; }

        public int directorId {  get; set; }
    }
}
