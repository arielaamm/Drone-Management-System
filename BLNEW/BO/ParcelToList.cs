using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int ID { set; get; }
        public string SenderName { set; get; }
        public string TargetName { set; get; }
        public Weight Weight { set; get; }
        public Priority Priority { set; get; }
        public Status status { set; get; }

    }//done
}
