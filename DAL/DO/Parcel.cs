using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;
namespace DO
{
    /// <summary>
    /// Parcel in DAL
    /// </summary>
    public struct Parcel
    {
        public int ? ID { set; get; }
        public int SenderId { set; get; }
        public int TargetId { set; get; }
        public Weight Weight { set; get; }
        public Priority Priority { set; get; }
        public int DroneId { set; get; }
        //added later
        public DateTime? Requested { set; get; } // זמן יצירת חבילה למשלוח
        public DateTime? Scheduled { set; get; }// זמן שיוך החבילה לרחפן 
        public DateTime? PickedUp { set; get; }//זמן אסיפת החבילה מהשולח
        public DateTime? Deliverd { set; get; }//זמן נתינת המשלוח ללקוח
    }
}
