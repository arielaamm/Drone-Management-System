using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
    {
        public int ID { set; get; }
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public Location location { set; get; }
        public List<ParcelInCustomer> fromCustomer { set; get; }
        public List<ParcelInCustomer> toCustomer { set; get; }

        //done
    }
}
