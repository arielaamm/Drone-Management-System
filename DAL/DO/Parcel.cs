using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DO
{
    class Parcel
    {
        public int ID { set; get; }
        public int SenderId { set; get; }
        public int TargetId { set; get; }
        public int DroneId { set; get; }
    
    }
}
