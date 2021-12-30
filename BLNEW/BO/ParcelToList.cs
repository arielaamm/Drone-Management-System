using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class ParcelToList
    {
        public int ID { set; get; }
        public string SenderName { set; get; }
        public string TargetName { set; get; }
        public WEIGHT Weight { set; get; }
        public PRIORITY Priority { set; get; }
        public STATUS status { set; get; }

    }//done
}
