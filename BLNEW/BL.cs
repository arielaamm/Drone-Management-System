using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IDAL;
using BLExceptions;
using DateTime = System.DateTime;
using IBL.BO;
using System.Runtime.Serialization;
namespace BL
{
    public class BL : IBL.IBL
    {    
        IDal dal = new DalObject();
        public BL()
        {
            //ניתן להעזר ראה ParcelNotAssociatedList() בגל אובגקט
            IEnumerable<IDAL.DO.Parcel> p = dal.Parcellist();
            List<IDAL.DO.Drone> d = DataSource.drones;
            IDAL.DO.Drone t = new IDAL.DO.Drone();
            foreach (IDAL.DO.Parcel i in p)
            {
                foreach (IDAL.DO.Drone item in d)
                {                
                    if ((i.Deliverd==DateTime.MinValue)&&(i.DroneId==item.ID))
                    {
                        t=item;
                        t.Status = (IDAL.DO.STATUS)1;
                        //לא עשיתי צריך לעשות
                    }
                }
            }
        }
        public double Distans(Location a, Location b)
        {
             return Math.Sqrt((Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2)));
        }
        //add functaions:
        //---------------------------------------------------------------------------------
        public void AddStation(int id, string name, Location location, int ChargeSlots)
        {

            IDAL.DO.Station tempStation = new IDAL.DO.Station()
            {
                ID = id,
                StationName = name,
                Longitude = location.Longitude,
                Lattitude = location.Lattitude,
                ChargeSlots = ChargeSlots,
            };
            try
            {
                dal.AddStation(tempStation);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException($"{ex.Message}");
            }
        }
        public void AddDrone(int id, string name, IBL.BO.WEIGHT weight,int IDStarting)
        {
            try
            {
                Random random = new Random();
                IDAL.DO.Drone tempDrone = new IDAL.DO.Drone()
                {
                    ID = id,
                    Model = name,
                    Weight = (IDAL.DO.WEIGHT)weight,
                    Buttery = random.Next(20, 40),
                    haveParcel = true,
                };
                IDAL.DO.DroneCharge tempDroneCharge = new IDAL.DO.DroneCharge()
                {
                    DroneId = id,
                    StationId = IDStarting,
                };

                dal.AddDrone(tempDrone);
                dal.AddDroneCharge(tempDroneCharge);
            }
            catch(Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }

        }
        public void AddCustomer(int id, string name, string PhoneNumber, Location location)
        {
            try
            {
                IDAL.DO.Customer tempCustomer = new IDAL.DO.Customer()
                {
                    ID = id,
                    CustomerName = name,
                    Longitude = location.Longitude,
                    Lattitude = location.Lattitude,
                    Phone = PhoneNumber,
                };

                dal.AddCustomer(tempCustomer);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }
        public void AddParcel(int SenderId, int TargetId, IBL.BO.WEIGHT weight, IBL.BO.PRIORITY Priority)
        {
            int? dID = null;
            foreach (var item in dal.Dronelist())
            {
                if (!(item.haveParcel))
                {
                    dID = item.ID;
                }
            }
            try
            {
                IDAL.DO.Parcel tempParcel = new IDAL.DO.Parcel()
                {
                    SenderId = SenderId,
                    TargetId = TargetId,
                    Weight = (IDAL.DO.WEIGHT)weight,
                    Priority = (IDAL.DO.PRIORITY)Priority,
                    Requested = DateTime.Now,
                    Scheduled =DateTime.MinValue,
                    PickedUp = DateTime.MinValue,
                    Deliverd = DateTime.MinValue,
                    DroneId = dID,
                };
            
                dal.AddParcel(tempParcel);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }
        //---------------------------------------------------------------------------------
        //updating functions:
        //---------------------------------------------------------------------------------
        public void UpdateDrone(int id, string name)//done
        {
            IDAL.DO.Drone d = dal.FindDrone(id);
            d.Model = name;
            foreach (var item in DataSource.drones)
            {
                if (item.ID == id)
                {
                    DataSource.drones.Remove(item);
                }
            }
            dal.AddDrone(d);
        }
#nullable enable
        public void UpdateStation(int id,string ? name ,int  TotalChargeslots)
        {
            IDAL.DO.Station s = dal.FindStation(id);
            s.StationName = name;
            s.ChargeSlots = TotalChargeslots;
            foreach (var item in DataSource.stations)
            {
                if (item.ID == id)
                {
                    DataSource.stations.Remove(item);
                }
            }
            dal.AddStation(s);
        }
        public void UpdateCustomer(int id, string ?NewName, string? NewPhoneNumber)
        {
            IDAL.DO.Customer c = dal.FindCustomers(id);
            c.CustomerName = NewName;
            c.Phone = NewPhoneNumber;
            foreach (var item in DataSource.customers)
            {
                if (item.ID == id)
                {
                    DataSource.customers.Remove(item);
                }
            }
            dal.AddCustomer(c);
        }
#nullable disable
        public void DroneToCharge(int id)
        {
            IDAL.DO.Drone d = dal.FindDrone(id);
            if ((d.Status != 0) || (d.Buttery < 20))
            {
                throw new DontHaveEnoughPowerException($"the drone {id} dont have enough power");
            }
            else
            {
                double distans = 0;
                int sID = 0;
                foreach (var item in stations())
                {
                    if (Distans(item.location,findDrone(id).current)>distans)
                    {
                        distans = Distans(item.location, findDrone(id).current);
                        sID = item.ID;
                    }
                }
                //מצב סוללה יעודכן בהתאם למרחק בין הרחפן לתחנה
                findDrone(id).current.Lattitude = findStation(sID).location.Lattitude;
                findDrone(id).current.Longitude = findStation(sID).location.Longitude;
                findDrone(id).Status = (STATUS)4;
                findStation(sID).FreeChargeSlots--; 
                dal.AddDroneCharge(sID, id);
                foreach (var item in DAL.DataSource.droneCharges)
                {
                    if (item.DroneId == id)
                    {
                        DroneCharging droneCharging1 = new()
                        {
                            ID = (int)item.DroneId,
                            Buttery = (dal.FindDrone((int)item.DroneId)).Buttery,
                        };
                        findStation(sID).DroneChargingInStation.Add(droneCharging1);
                        break;
                    }
                }
            }
        }
        public void DroneOutCharge(int id,int time)
        {
            
            if (findDrone(id).Status==(STATUS)4)
            {
                findDrone(id).Status = STATUS.FREE;
                findDrone(id).Buttery = (dal.Power()[4]) * (time);
                foreach (var item in DAL.DataSource.droneCharges)
                {
                    if (item.DroneId==id)
                    {
                        findStation(item.StationId).FreeChargeSlots++;
                        DataSource.droneCharges.Remove(item); 
                        foreach (var item1 in findStation(item.StationId).DroneChargingInStation)
                        {
                            if (item1.ID==id)
                            {
                                findStation(item.StationId).DroneChargingInStation.Remove(item1);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new DroneDontInCharging($"The Drone {id} Doesn't In Charging");
            }
        }
        public void AttacheDrone(int id)
        {
            if (!(findDrone(id).haveParcel))
            {
                List<Parcel> temp = parcelsNotAssociated().ToList();
                List<Parcel> temp1 = temp.FindAll( delegate (Parcel p) {return p.Priority == PRIORITY.SOS;});
                if (temp1.Count==0)
                {
                    temp1=temp.FindAll(delegate (Parcel p) { return p.Priority == PRIORITY.FAST; });
                    if (temp1.Count==0)
                    {
                        temp1=temp.FindAll(delegate (Parcel p) { return p.Priority == PRIORITY.REGULAR; });
                        if (temp1.Count==0)
                        {
                            throw new ThereIsNoParcel("there are no parcel");
                        }
                    }
                }
                temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == PRIORITY.SOS; });
                if (temp1.Count == 0)
                {
                    temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == PRIORITY.FAST; });
                    if (temp1.Count == 0)
                    {
                        temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == PRIORITY.REGULAR; });
                        if (temp1.Count == 0)
                        {
                            throw new ThereIsNoParcel("there are no parcel");
                        }
                    }
                }
                Location location = new()
                { Lattitude=0,Longitude=0, };
                int saveID = 0;//בטוח ידרס
                foreach (var item in temp1)
                {
                    if (Distans(findDrone(id).current, findcustomer(item.sender.ID).location) > Distans(findDrone(id).current,location))
                    {
                        location.Lattitude = findcustomer(item.sender.ID).location.Lattitude;
                        location.Longitude = findcustomer(item.sender.ID).location.Longitude;
                        saveID = item.ID;
                    }
                }
                Parcel p = findparcel(saveID);
            }
        }
        public void PickUpParcel(int id)
        {

        }
        public void Parceldelivery(int id)
        {

        }
        //-----------------------------------------------------------------------------
        //display func
        //------------------------------------------------------------------------------
        public Station findStation(int id)//סיימתי
        {
            
            IDAL.DO.Station s = dal.FindStation(id);
            Location temp = new Location() 
            { 
                Lattitude=s.Lattitude,
                Longitude=s.Longitude,
            };
            List<DroneCharging> droneChargingTemp = new();
            foreach (var item in DataSource.droneCharges)
            {
                if (item.StationId == id)
                {
                    DroneCharging droneCharging1 = new()
                    {
                        ID = (int)item.DroneId,
                        Buttery = (dal.FindDrone((int)item.DroneId)).Buttery,
                    };
                    droneChargingTemp.Add(droneCharging1);
                }
            }
            Station newStation = new Station()
            {
                ID = (int)s.ID,
                StationName = s.StationName,
                location = temp,
                FreeChargeSlots = s.ChargeSlots,
                DroneChargingInStation = droneChargingTemp,
            };
            return newStation;
        }
        public Drone findDrone(int id)//סיימתי
        {
            IDAL.DO.Drone d = dal.FindDrone(id);
            ParcelTransactining parcelTransactiningTemp = new();
            if (d.Status == IDAL.DO.STATUS.BELONG)
            {
                IDAL.DO.Parcel p = new();
                foreach (var item in DataSource.parcels)
                {
                    if (item.DroneId == id)
                        p = item;
                }
                IDAL.DO.Customer s = dal.FindCustomers(p.SenderId);
                IDAL.DO.Customer t = dal.FindCustomers(p.TargetId);
                CustomerInParcel send = new()
                {
                    ID=(int)s.ID,
                    CustomerName = s.CustomerName,
                };
                CustomerInParcel target = new()
                {
                    ID = (int)t.ID,
                    CustomerName = t.CustomerName,
                };
                Location locationSend = new() 
                { 
                    Lattitude = s.Lattitude,
                    Longitude = s.Longitude,
                };
                Location locationTarget = new()
                {
                    Lattitude = t.Lattitude,
                    Longitude = t.Longitude,
                };
                
                parcelTransactiningTemp.ID = (int)p.ID;
                parcelTransactiningTemp.ParcelStatus = p.PickedUp == DateTime.MinValue;
                parcelTransactiningTemp.priority = (PRIORITY)p.Priority;
                parcelTransactiningTemp.weight = (WEIGHT)p.Weight;
                parcelTransactiningTemp.sender = send;
                parcelTransactiningTemp.target = target;
                parcelTransactiningTemp.Lsender = locationSend;
                parcelTransactiningTemp.Ltarget = locationTarget;
                parcelTransactiningTemp.distance = Distans(locationSend, locationTarget);
            }
            Drone newStation = new Drone()
            {
                haveParcel = d.haveParcel,
                ID = (int)d.ID,
                Model = d.Model,
                Weight = (WEIGHT)d.Weight,
                Status = (STATUS)d.Status,
                Buttery = d.Buttery,
                ///לברר איך מוצאים מיקום של רחפן
                parcel= parcelTransactiningTemp,
            };
            return newStation;
        }
        public Parcel findparcel(int id)//סיימתי
        {
            IDAL.DO.Parcel p = dal.FindParcel(id);//לסייפ מימוש
            IDAL.DO.Customer s = dal.FindCustomers(p.SenderId);
            IDAL.DO.Customer t = dal.FindCustomers(p.TargetId);
            CustomerInParcel send = new()
            {
                ID = (int)s.ID,
                CustomerName = s.CustomerName,
            };
            CustomerInParcel target = new()
            {
                ID =(int)t.ID,
                CustomerName = t.CustomerName,
            }; 
            IDAL.DO.Drone d = dal.FindDrone((int)p.DroneId);
            DroneInParcel droneInParcelTemp = new() 
            { 
                ID=(int)d.ID,
                Buttery = d.Buttery,
                ///לברר מיקום של רחפן
            };
            Parcel newParcel = new Parcel()
            {
                ID = (int)p.ID,
                sender = send,
                target = target,
                Weight = (WEIGHT)p.Weight,
                Priority = (PRIORITY)p.Priority,
                Drone = droneInParcelTemp,
                Requested = p.Requested,
                Scheduled = p.Scheduled,
                PickedUp = p.PickedUp,
                Deliverd = p.Deliverd,

            };
            return newParcel;
        }
        public Customer findcustomer(int id)//fliping done
        {
            IDAL.DO.Customer c = dal.FindCustomers(id);
            IEnumerable<IDAL.DO.Parcel> p = dal.Parcellist();
            Location temp = new Location()
            {
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
            };
            Customer newCustomer = new Customer()
            {
                ID=(int)c.ID,
                CustomerName = c.CustomerName,
                Phone= c.Phone,
                location = temp,

            };
            ParcelInCustomer fromCustomer = new();
            ParcelInCustomer ToCustomer = new();
            foreach (var item in p)
            {
                if (item.SenderId == id)
                {
                    fromCustomer.ID = (int)item.ID;
                    fromCustomer.weight = (WEIGHT)item.Weight;
                    fromCustomer.priority = (PRIORITY)item.Priority;
                    if(item.Requested< DateTime.Now)
                    {
                        fromCustomer.status = (STATUS)0;
                    }
                    if (item.Scheduled < DateTime.Now)
                    {
                        fromCustomer.status = (STATUS)1;
                    }
                    if (item.PickedUp < DateTime.Now)
                    {
                        fromCustomer.status = (STATUS)2;
                    }
                    if (item.Deliverd < DateTime.Now)
                    {
                        fromCustomer.status = (STATUS)3;
                    }
                    fromCustomer.sender.ID = id;
                    fromCustomer.sender.CustomerName = newCustomer.CustomerName;
                    fromCustomer.target.ID = item.TargetId;
                    fromCustomer.target.CustomerName = dal.FindCustomers(item.TargetId).CustomerName;//bl??dl??
                    newCustomer.fromCustomer.Add(fromCustomer);
                }

                if ((item.TargetId == id)&& (item.Deliverd < DateTime.Now))
                {
                    ToCustomer.ID = (int)item.ID;
                    ToCustomer.weight = (WEIGHT)item.Weight;
                    ToCustomer.priority = (PRIORITY)item.Priority;
                    ToCustomer.status = (STATUS)3;
                    ToCustomer.sender.ID = item.TargetId;
                    ToCustomer.sender.CustomerName = dal.FindCustomers(item.TargetId).CustomerName;//bl??dl??
                    ToCustomer.target.ID = id;
                    ToCustomer.target.CustomerName = newCustomer.CustomerName;
                    newCustomer.toCustomer.Add(ToCustomer);
                }
            }
            return newCustomer;
        }
        //-----------------------------------------------------------------------------------
        //listView func
        //-----------------------------------------------------------------------------------------
        public IEnumerable<Station> stations()
        {
            List<Station> temp = new();
            foreach (var item in dal.Stationlist())
            {
                temp.Add(findStation((int)item.ID));
            }
            return temp;
        }
        public IEnumerable<Drone> drones()
        {
            List<Drone> temp = new();
            foreach (var item in dal.Dronelist())
            {
                temp.Add(findDrone((int)item.ID));
            }
            return temp;
        }
        public IEnumerable<Parcel> parcels()
        {
            List<Parcel> temp = new();
            foreach (var item in dal.Parcellist())
            {
                temp.Add(findparcel((int)item.ID));
            }
            return temp;
        }
        public IEnumerable<Customer> customers()
        {
            List<Customer> temp = new();
            foreach (var item in dal.Customerlist())
            {
                temp.Add(findcustomer((int)item.ID));
            }
            return temp;
        }
        public IEnumerable<Parcel> parcelsNotAssociated()
        {
            List<Parcel> temp = new();
            foreach (var item in dal.ParcelNotAssociatedList())
            {
                temp.Add(findparcel((int)item.ID));
            }
            return temp;
        }
        public IEnumerable<Station> FreeChargeslots()
        {
            List<Station> temp = new();
            foreach (var item in dal.Freechargeslotslist())
            {
                temp.Add(findStation((int)item.ID));
            }
            return temp;
        }
        //-----------------------------------------------------------------------------------------
    }
}