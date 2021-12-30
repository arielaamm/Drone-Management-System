using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class ParcelTransactining
    {
        public int ID { set; get; }
        public bool ParcelStatus { set; get; } // on the way - true, waitnig - false
        public PRIORITY priority { set; get; }
        public WEIGHT weight { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }
        public Location Lsender { set; get; }
        public Location Ltarget { set; get; }
        public double distance { set; get; }
        //done

    }
}
