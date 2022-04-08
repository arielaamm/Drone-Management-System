using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Customer in BL
    /// </summary>
    public class Customer
    {
        public bool IsActive { set; get; }
        public int ID { set; get; }
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public Location Position { set; get; }
        public List<ParcelInCustomer> fromCustomer { set; get; }
        public List<ParcelInCustomer> toCustomer { set; get; }

        //done
    }
}
