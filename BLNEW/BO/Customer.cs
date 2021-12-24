using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    class Customer
    {
        public int ID { set; get; }
        public string CustomerName { set; get; }
        public string Phone { set; get; }
        public Location location { set; get; }
        public ParcelInCustomer fromCustomer { set; get; }
        public ParcelInCustomer toCustomer { set; get; }
        //done
        //רשימת משלוחיםחבילות אצל לקוח - מהלקוח??????????????????????????
    //רשימת משלוחיםחבילות אצל לקוח - אל הלקוח??????????????????????????
    }
}
