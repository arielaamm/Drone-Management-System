using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BO
{
    class Parcel
    {
        public int ID { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel target { set; get; }
        public WEIGHT Weight { set; get; }
        public PRIORITY Priority { set; get; }
        public DroneInParcel Drone { set; get; }
        public DateTime Requested { set; get; }//יצירה
        public DateTime Scheduled { set; get; }//שיוך
        public DateTime PickedUp { set; get; }//איסוף
        public DateTime Deliverd { set; get; }//אספקה

    }//done
}
