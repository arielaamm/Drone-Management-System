using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;
namespace IDAL.DO
{
    struct Parcel
    {
        public int ID { set; get; }
        public int SenderId { set; get; }
        public int TargetId { set; get; }
        public WEIGHT Weight { set; get; }
        public PRIORITY Priority { set; get; }
        public int DroneId { set; get; }

    
    }
}
