using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Randon = System.Random;
using DALExceptionscs;
namespace DAL
{
    //static
    public class DalObject : IDAL.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
        public double[] Power()
        {
            double[] a = {
                DataSource.Config.free,
                DataSource.Config.light,
                DataSource.Config.medium,
                DataSource.Config.heavy,
                DataSource.Config.ChargePerHour };
            return a;
        }
        #region add (1)
        public void AddStation(Station s)
        {         
            DataSource.staticId++;
            DataSource.stations.Add(s);
        }

        public void AddDrone(Drone d)
        {        
            DataSource.staticId++;
            DataSource.drones.Add(d);
            Station s = new Station();
            foreach (var item in DataSource.stations)
            {
                if (item.ChargeSlots != 0)
                {
                    s = item;
                    break;
                }
            }
            DroneCharge temp = new DroneCharge()
            {
                DroneId = d.ID,
                StationId = (int)s.ID,
            };
            AddDroneCharge(temp);
        }

        public void AddCustomer(Customer c)
        { 
            DataSource.staticId++;
            DataSource.customers.Add(c);
        }

        public void AddParcel(Parcel p)
        {
            p.ID = DataSource.staticId;
            DataSource.staticId++;
            DataSource.parcels.Add(p);
        }

        public void AddDroneCharge(int DroneId, int StationId)
        {
            DroneCharge d = new DroneCharge();
            d.DroneId = DroneId;
            d.StationId = StationId;
            DataSource.droneCharges.Add(d);
        }

        public void AddDroneCharge(DroneCharge d)
        {
            DataSource.droneCharges.Add(d);
        }
        #endregion
        #region update (2)
        public void AttacheDrone(int parcelID)
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
                if ((i.Status == 0)&&(i.Weight>p.Weight))
                {
                    p.DroneId = i.ID;
                    p.Scheduled = DateTime.Now;
                    d = i;
                    d.Status = (Status)0;
                    break;
                }
            }
        }

        public void PickParcel(int parcelID)
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
                    keeper = (int)i.ID;
                    break;
                }
            }
            Drone d = FindDrone(keeper);
            d.Status = (Status)2;

        }

        public void ParcelToCustomer(int parcelID)
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
                if (i.ID == p.TargetId)
                {
                    p.Deliverd = DateTime.Now;
                    keeper = (int)p.DroneId;
                    break;
                }
            }
            Drone d = FindDrone(keeper);
            d.Status = (Status)0;

        }

        public void DroneToCharge(int droneID, int stationID)
        {
            Drone d = new();
            int index = -1;
            foreach (var i in DataSource.drones)
            {
                if (i.ID == droneID)
                {
                    d = i;
                    break;
                }
                index++;

            }
            d.Status = (Status)1;
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

        public void DroneOutCharge(int droneID)
        {
            Drone d = new();
            int index = -1;
            foreach (var i in DataSource.drones)
            {
                if (i.ID == droneID)
                {
                    d = i;
                    break;
                }
                index++;
            }
            d.Status = (Status)0;
            DataSource.drones.RemoveAt(index);
            DataSource.drones.Insert(index , d);
            index = 0;
            ;
            foreach (var i in DataSource.droneCharges)
            {
                if (d.ID == droneID)
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

        public Station FindStation(int id)
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

        public Drone FindDrone(int id)
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

        public Customer FindCustomers(int id)
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

        public Parcel FindParcel(int id)
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
        public IEnumerable<Station> Stationlist() => DataSource.stations;

        public IEnumerable<Customer> Customerlist() => DataSource.customers;

        public IEnumerable<Parcel> Parcellist() => DataSource.parcels;

        public IEnumerable<Drone> Dronelist() => DataSource.drones;

        public IEnumerable<Parcel> ParcelNotAssociatedList()
        {
            List<Parcel> notassociated = new();
            foreach (var i in DataSource.parcels)
            {
                if (i.DroneId == 0) 
                {
                    notassociated.Add(i);
                }
            }
            return notassociated;
        }

        public IEnumerable<Station> Freechargeslotslist()
        {
            List<Station> Freechargeslots = new();
            foreach (var i in DataSource.stations)
            {
                if (i.ChargeSlots > 0)
                {
                    Freechargeslots.Add(i);
                }
            }
            return Freechargeslots;
        }
        #endregion
    }
}

