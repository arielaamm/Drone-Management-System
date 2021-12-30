using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    public class ParcelInCustomer
    {
        public int ID { set; get; }
        public WEIGHT weight { set; get; }
        public PRIORITY priority { set; get; }
        public STATUS status { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }

        //done
    }
}
