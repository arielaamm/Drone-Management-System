using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelTransactioning
    {
        public int ?ID { set; get; }
        public bool ParcelStatus { set; get; } // on the way - true, waitnig to drone arrival - false
        public Priority priority { set; get; }
        public Weight weight { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }
        public Location Lsender { set; get; }
        public Location Ltarget { set; get; }
        public double distance { set; get; }
        //done

    }
}
