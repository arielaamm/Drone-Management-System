using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
namespace DAL
{
    //static
    public class DalObject : IDAL.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        public double[] power()
        {
            /*צריך לממש*/
            double[] a= {};
            return a;
        }
        #region add (1)
        static Random random = new Random();
        public /*static*/ void AddStation(Station s)
        {
            DataSource.staticId++;
            DataSource.stations.Add(s);
        }

        public /*static*/ void AddDrone(Drone d)
        {
            DataSource.staticId++;
            DataSource.drones.Add(d);
            Station s = new Station();
            //foreach (var item in DataSource.stations)
            //{
            //    if (item.ChargeSlots!=0)
            //    {
            //        s = item;
            //        break;
            //    }
            //}
            //DroneCharge temp = new DroneCharge()
            //{
            //    DroneId = d.ID,
            //    StationId = s.ID,
            //};
        }

        public /*static*/ void AddCustomer(Customer c)
        {
            DataSource.staticId++;
            DataSource.customers.Add(c);
        }

        public /*static*/ void AddParcel(Parcel parcel)
        {
            parcel.ID = DataSource.staticId;
            DataSource.staticId++;
            DataSource.parcels.Add(parcel);
        }

        public /*static*/ void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new DroneCharge();
            d.DroneId = DroneId;
            d.StationId = StationId;
            DataSource.droneCharges.Add(d);
        }
        public /*static*/ void AddDroneCharge(DroneCharge d)
        {
            DataSource.droneCharges.Add(d);
        }
        #endregion
        #region update (2)
        public /*static*/ void AttacheDrone(int parcelID)
        {
            Parcel p = new(); 
            Drone d = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.ID == parcelID)
                {
                    p = i;
                    break;
                }
            }
            foreach (var i in DataSource.drones)
            {
                if ((i.Status == 0)&&(i.Weight>p.Weight))//battery?
                {
                    p.DroneId = i.ID;
                    p.Scheduled = DateTime.Now;//לעדכן רחפן
                    d = i;
                    d.Status = (STATUS)0;
                    break;
                }
            }
        }

        public /*static*/ void PickParcel(int parcelID)
        {
            Parcel p = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.ID == parcelID)
                {
                    p = i;
                    break;
                }
            }
            int keeper = 0;
            foreach (var i in DataSource.drones)
            {

                if (i.ID == p.DroneId)
                {
                    p.PickedUp = DateTime.Now;
                    keeper = i.ID;
                    break;
                }
            }
            Drone d = FindDrone(keeper);
            d.Status = (STATUS)2;

        }

        public /*static*/ void ParcelToCustomer(int parcelID)
        {
            Parcel p = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.ID == parcelID)
                {
                    p = i;
                    break;
                }
            }
            int keeper = 0;
            foreach (var i in DataSource.customers)
            {

                if (i.ID == p.TargetId)// == DataSource.customers)
                {
                    p.Deliverd = DateTime.Now;
                    keeper = p.DroneId;
                    break;
                }
            }
            Drone d = FindDrone(keeper);
            d.Status = (STATUS)0;

        }

        public /*static*/ void DroneToCharge(int droneID, int stationID)//station name\id???? 
        {
            Drone d = new();
            int index = -1;
            foreach (var i in DataSource.drones)
            {
                if (i.ID == droneID)// == DataSource.customers)
                {
                    d = i;
                    break;
                }
                index++;

            }
            d.Status = (STATUS)1;
            Station s = new();
            foreach (var i in DataSource.stations)
            {
                if (i.ID == stationID)
                {
                    s = i;
                    break;
                }
            }
            s.ChargeSlots++;
            DataSource.drones.RemoveAt(index);
            DataSource.drones.Insert(index, d);
            AddDroneCharge(droneID, stationID);
        }

        public /*static*/ void DroneOutCharge(int droneID)//station name\id???? 
        {
            Drone d = new();
            int index = -1;
            foreach (var i in DataSource.drones)
            {
                if (i.ID == droneID)// == DataSource.customers)
                {
                    d = i;
                    break;
                }
                index++;
            }
            d.Status = (STATUS)0;
            DataSource.drones.RemoveAt(index);
            DataSource.drones.Insert(index , d);
            index = 0;
            ;
            foreach (var i in DataSource.droneCharges)
            {
                if (d.ID == droneID)// == DataSource.customers)
                {
                    Station s = new();
                    foreach (var o in DataSource.stations)
                    {
                        if (o.ID == i.StationId)
                        {
                            s = o;
                            break;
                        }
                    }
                    s.ChargeSlots--;
                    DataSource.droneCharges.RemoveAt(index);
                    break;
                }
                index++;
            }

        }

        #endregion
        #region print(3)

        public /*static*/ Station FindStation(int id)
        {
            Station s = new Station();
            foreach (var i in DataSource.stations)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return s;
        }

        public /*static*/ Drone FindDrone(int id)
        {
            Drone d = new Drone();
            foreach (var i in DataSource.drones)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return d;
        }

        public /*static*/ Customer FindCustomers(int id)
        {
            Customer c = new Customer();

            foreach (var i in DataSource.customers)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return c;
        }

        public /*static*/ Parcel FindParcel(int id)
        {
            Parcel p = new Parcel();
            foreach (var i in DataSource.parcels)
            {
                if (i.ID == id)
                {
                    return i;
                }
            }
            return p;
        }

        #endregion
        #region print lists (4)
        public /*static*/ IEnumerable<Station> Stationlist() => DataSource.stations;

        public /*static*/ IEnumerable<Customer> Customerlist() => DataSource.customers;

        public /*static*/ IEnumerable<Parcel> Parcellist() => DataSource.parcels;

        public /*static*/ IEnumerable<Drone> Dronelist() => DataSource.drones;

        public /*static*/ IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            List<Parcel> notassociated = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.DroneId == 0) // חבילה שלא שויכה לרחפן מוגדרת בקונפיג שה אידי של הרחפן שלה הוא 0
                {
                    notassociated.Add(i);
                }
            }
            return notassociated;
        }

        public /*static*/ IEnumerable<Station> Freechargeslotslist()
        {
            List<Station> Freechargeslots = new();
            foreach (var i in DataSource.stations)
            {
                if (i.ChargeSlots > 0) // חבילה שלא שויכה לרחפן מוגדרת בקונפיג שה אידי של הרחפן שלה הוא 0
                {
                    Freechargeslots.Add(i);
                }
            }
            return Freechargeslots;
        }
        #endregion
    }
}

