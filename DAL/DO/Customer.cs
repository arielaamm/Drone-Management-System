using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Customer
    {
        public int? ID { set; get; }
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public double Longitude { set; get; }
        public double Lattitude { set; get;}

    }
}
