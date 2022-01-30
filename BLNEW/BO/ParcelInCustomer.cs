using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelInCustomer
    {
        public int ID { set; get; }
        public Weight weight { set; get; }
        public Priority priority { set; get; }
        public Status status { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }

        //done
    }
}
