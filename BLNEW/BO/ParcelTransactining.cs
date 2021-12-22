using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    class ParcelTransactining
    {
        public int ID { set; get; }
        public PRIORITY priority { set; get; }
        public WEIGHT weight { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }
        public Location Lsender { set; get; }
        public Location Ltarget { set; get; }
        //distance???????

    }
}
