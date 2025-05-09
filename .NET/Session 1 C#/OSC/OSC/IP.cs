using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OSC
{
    public class IP
    {
        private int[] Segments = new int[4];

        public int this[int index] //Indexer Proprety
        {
            get { return Segments[index]; }
            set { Segments[index] = value; }
        }

        public IP(string IpAddress)
        {
            var segs = IpAddress.Split('.');
            for (int i = 0; i < segs.Length; i++)
            {
                Segments[i] = int.Parse(segs[i]);
            }
        }
        public IP(int Segment1, int Segment2, int Segment3, int Segment4) 
        {
            this.Segments[0] = Segment1;
            this.Segments[1] = Segment2;
            this.Segments[2] = Segment3;
            this.Segments[3] = Segment4;
        }

        public string Address() => string.Join(".", this.Segments);

    }
}
