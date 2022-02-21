using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Station To List
    /// Display objrct for the next layer
    /// </summary>
    public class StationToList
    {
        public int ID { set; get; }
        public string StationName { set; get; }
        public int FreeChargeSlots { set; get; }
        public int UsedChargeSlots { set; get; }

    }
}
