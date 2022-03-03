using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Parcel in Customer 
    /// sub object 
    /// </summary>
    public class ParcelInCustomer
    {
        public int ID { set; get; }
        public Weight Weight { set; get; }
        public Priority Priority { set; get; }
        public StatusParcel Status { set; get; }
        public CustomerInParcel Sender { set; get; }
        public CustomerInParcel Target { set; get; }

        //done
    }
}
