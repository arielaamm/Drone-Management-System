using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInCustomer
    {
        public int ID { set; get; }
        public Weight Weight { set; get; }
        public Priority Priority { set; get; }
        public Status Status { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Target { set; get; }

        //done
    }
}
