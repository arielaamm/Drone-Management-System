using BLExceptions;
using DAL;
using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;
namespace BL
{
    public class BL : IBL.IBL
    {
        IDal dal = new DalObject();
        /// <summary>
        /// constractor
        /// </summary>
        public BL()
        {
            IEnumerable<IDAL.DO.Parcel> p = dal.Parcellist();
            List<IDAL.DO.Drone> d = DataSource.drones;

            foreach (IDAL.DO.Parcel i in p)
            {
                Drone tempDrone = new Drone();
                if ((i.Scheduled != null) && (i.Deliverd == null) && (i.DroneId != 0))//Deliverd==0???
                {
                    tempDrone = FindDrone(i.SenderId);
                    FindDrone(i.SenderId).Status = (Status)2;
                    if ((i.PickedUp == null) && (i.Scheduled != null))
                    {//shortest station
                        Location sta = new();
                        foreach (IDAL.DO.Station item in dal.Stationlist())
                        {
                            if (Distans(FindDrone(i.SenderId).Position, FindDrone((int)i.ID).Position) > Distans(FindDrone((int)i.ID).Position, sta))
                            {
                                sta = FindDrone(i.SenderId).Position;
                            }
                        }
                    }
                    if ((i.Deliverd == null) && (i.PickedUp != null))
                    {
                        FindDrone(i.SenderId).Position = FindDrone(i.SenderId).Position;
                    }
                    Random random = new Random();
                    if (i.Scheduled == null)
                    {
                        if (random.Next(1, 2) == 1)
                        {
                            FindDrone(i.SenderId).Status = Status.CREAT;
                            List<Parcel> pa = new();
                            foreach (var item in parcels())
                            {
                                pa = pa.FindAll(delegate (Parcel p) { return (p.Deliverd != null); });//לקוח שקיבל חבילה
                            }
                            FindDrone(i.SenderId).Position = Findcustomer(pa[random.Next(0, pa.Count - 1)].target.ID).location;//מספר רנדומלי מתוך כל הלקוחות שקיבלו חבילה בו אני מחפש את האיידיי של המקבל שם בחיפוש לקוח ולוקח ממנו את המיקום
                            FindDrone(i.SenderId).Battery = random.Next(20, 100);

                        }
                        else
                        {
                            List<Station> s = new();
                            FindDrone(i.SenderId).Position = freeChargeslots().ToList()[random.Next(0, (freeChargeslots().Count()) - 1)].location;
                            FindDrone(i.SenderId).Battery = random.Next(0, 20);
                            FindDrone(i.SenderId).Status = Status.MAINTENANCE;
                        }

                    }
                }
            }
        }
        
        /// <summary>
        /// Distans
        /// </summary>
        /// <returns>Distans between a - b</returns>
        public double Distans(Location a, Location b)
        {
            return Math.Sqrt((Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2)));
        }
        
        //add functaions:
        //---------------------------------------------------------------------------------
        
        /// <summary>
        /// Add station
        /// </summary>
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
        
        /// <summary>
        /// Add drone
        /// </summary>
        public void AddDrone(int id, string name, IBL.BO.Weight weight, int IDStarting)
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
                FindStation(IDStarting).FreeChargeSlots--;
                dal.AddDrone(tempDrone);
                dal.AddDroneCharge(tempDroneCharge);
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }

        }
        
        /// <summary>
        /// Add customer
        /// </summary>
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
        
        /// <summary>
        /// Add parcel
        /// </summary>
        public void AddParcel(int SenderId, int TargetId, IBL.BO.Weight weight, IBL.BO.Priority Priority)
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
                    Scheduled = null,
                    PickedUp = null,
                    Deliverd = null,
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
        
        /// <summary>
        /// Update Drone model
        /// </summary>
        public void UpdateDrone(int id, string name)
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
        
        /// <summary>
        /// Update Station details
        /// </summary>
#nullable enable
        public void UpdateStation(int id, string? name, int TotalChargeslots)
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
        
        /// <summary>
        /// Update Custemer details
        /// </summary>
        public void UpdateCustomer(int id, string? NewName, string? NewPhoneNumber)
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
        
        /// <summary>
        /// Inserting a drone from a charger
        /// </summary>
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
                    if (Distans(item.location, FindDrone(id).Position) > distans)
                    {
                        distans = Distans(item.location, FindDrone(id).Position);
                        sID = item.ID;
                    }
                }
                //מצב סוללה יעודכן בהתאם למרחק בין הרחפן לתחנה
                FindDrone(id).Position.Lattitude = FindStation(sID).location.Lattitude;
                FindDrone(id).Position.Longitude = FindStation(sID).location.Longitude;
                FindDrone(id).Status = (Status)4;
                FindStation(sID).FreeChargeSlots--;
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
                        FindStation(sID).DroneChargingInStation.Add(droneCharging1);
                        break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Removing a drone from a charger
        /// </summary>
        public void DroneOutCharge(int id, int time)
        {

            if (FindDrone(id).Status == (Status)4)
            {
                FindDrone(id).Status = Status.CREAT;
                FindDrone(id).Battery = (dal.Power()[4]) * (time);
                foreach (var item in DAL.DataSource.droneCharges)
                {
                    if (item.DroneId == id)
                    {
                        FindStation(item.StationId).FreeChargeSlots++;
                        DataSource.droneCharges.Remove(item);
                        foreach (var item1 in FindStation(item.StationId).DroneChargingInStation)
                        {
                            if (item1.ID == id)
                            {
                                FindStation(item.StationId).DroneChargingInStation.Remove(item1);
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
        
        /// <summary>
        /// Assign a parcel to a drone
        /// </summary>
        public void AttacheDrone(int id)
        {
            if (!(FindDrone(id).HasParcel))
            {
                List<Parcel> temp = parcelsNotAssociated().ToList();
                List<Parcel> temp1 = temp.FindAll(delegate (Parcel p) { return p.Priority == Priority.SOS; });
                if (temp1.Count == 0)
                {
                    temp1 = temp.FindAll(delegate (Parcel p) { return p.Priority == Priority.FAST; });
                    if (temp1.Count == 0)
                    {
                        temp1 = temp.FindAll(delegate (Parcel p) { return p.Priority == Priority.REGULAR; });
                        if (temp1.Count == 0)
                        {
                            throw new ThereIsNoParcel("there are no parcel");
                        }
                    }
                }
                temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == Priority.SOS; });
                if (temp1.Count == 0)
                {
                    temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == Priority.FAST; });
                    if (temp1.Count == 0)
                    {
                        temp1 = temp1.FindAll(delegate (Parcel p) { return p.Priority == Priority.REGULAR; });
                        if (temp1.Count == 0)
                        {
                            throw new ThereIsNoParcel("there are no parcel");
                        }
                    }
                }
                Location location = new()
                { Lattitude = 0, Longitude = 0, };
                int saveID = 0;//בטוח ידרס
                foreach (var item in temp1)
                {
                    if (Distans(FindDrone(id).Position, Findcustomer(item.sender.ID).location) > Distans(FindDrone(id).Position, location))
                    {
                        location.Lattitude = Findcustomer(item.sender.ID).location.Lattitude;
                        location.Longitude = Findcustomer(item.sender.ID).location.Longitude;
                        saveID = item.ID;
                    }
                }
                Findparcel(saveID).Drone.ID = id;
                Findparcel(saveID).Drone.Buttery = FindDrone(id).Battery;
                Findparcel(saveID).Drone.current = FindDrone(id).Position;
                Findparcel(saveID).Scheduled = DateTime.Now;
                FindDrone(id).Status = Status.BELONG;
            }
        }
        
        /// <summary>
        /// Collection of a parcel by drone
        /// </summary>
        public void PickUpParcel(int id)
        {
            if (FindDrone(id).Status == Status.BELONG)
            {
                switch (FindDrone(id).Weight)
                {
                    case Weight.LIGHT:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Position)) * (dal.Power()[(int)Weight.LIGHT]));
                        break;
                    case Weight.MEDIUM:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Position)) * (dal.Power()[(int)Weight.MEDIUM]));
                        break;
                    case Weight.HEAVY:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Position)) * (dal.Power()[(int)Weight.HEAVY]));
                        break;
                    case Weight.FREE:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Position)) * (dal.Power()[(int)Weight.FREE]));
                        break;
                }
                FindDrone(id).Position = FindDrone(id).Parcel.Lsender;
                Findparcel(FindDrone(id).Parcel.ID).PickedUp = DateTime.Now;
            }
            else
                throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have picked up");
        }
        
        /// <summary>
        /// Delivery of a parcel by drone
        /// </summary>
        public void Parceldelivery(int id)
        {
            if (FindDrone(id).Status == Status.PICKUP)
            {
                switch (FindDrone(id).Weight)
                {
                    case Weight.LIGHT:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Parcel.Ltarget)) * (dal.Power()[(int)Weight.LIGHT]));
                        break;
                    case Weight.MEDIUM:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Parcel.Ltarget)) * (dal.Power()[(int)Weight.MEDIUM]));
                        break;
                    case Weight.HEAVY:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Parcel.Ltarget)) * (dal.Power()[(int)Weight.HEAVY]));
                        break;
                    case Weight.FREE:
                        FindDrone(id).Battery = FindDrone(id).Battery - ((Distans(FindDrone(id).Parcel.Lsender, FindDrone(id).Parcel.Ltarget)) * (dal.Power()[(int)Weight.FREE]));
                        break;
                }
                FindDrone(id).Position = FindDrone(id).Parcel.Ltarget;
                FindDrone(id).Status = Status.CREAT;
                Findparcel(FindDrone(id).Parcel.ID).Deliverd = DateTime.Now;

            }
            else
                throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have delivered");

        }
        
        //-----------------------------------------------------------------------------
        //display func
        //------------------------------------------------------------------------------
        
        /// <summary>
        /// station search
        /// </summary>
        /// <returns>found station</returns>
        public Station FindStation(int id)//סיימתי
        {

            IDAL.DO.Station s = dal.FindStation(id);
            Location temp = new Location()
            {
                Lattitude = s.Lattitude,
                Longitude = s.Longitude,
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
                FreeChargeSlots = 5 - droneChargingTemp.Count,
                DroneChargingInStation = droneChargingTemp,
            };
            return newStation;
        }
        
        /// <summary>
        /// drone search
        /// </summary>
        /// <returns>found drone</returns>
        public Drone FindDrone(int id)//סיימתי
        {
            IDAL.DO.Drone d = dal.FindDrone(id);
            ParcelTransactioning parcelTransactiningTemp = new();
            Drone newStation = new Drone();
            newStation.HasParcel = d.haveParcel;
            newStation.ID = (int)d.ID;
            newStation.Model = d.Model;
            newStation.Weight = (Weight)d.Weight;
            newStation.Status = (Status)d.Status;
            newStation.Battery = d.Buttery;
            Location locationDrone = new()
            {
                Lattitude = d.Lattitude,
                Longitude = d.Longitude,
            };
            newStation.Position = locationDrone;

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
                    ID = (int)s.ID,
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
                parcelTransactiningTemp.ParcelStatus = p.PickedUp == null;
                parcelTransactiningTemp.priority = (Priority)p.Priority;
                parcelTransactiningTemp.weight = (Weight)p.Weight;
                parcelTransactiningTemp.sender = send;
                parcelTransactiningTemp.target = target;
                parcelTransactiningTemp.Lsender = locationSend;
                parcelTransactiningTemp.Ltarget = locationTarget;
                parcelTransactiningTemp.distance = Distans(locationSend, locationTarget);
                newStation.Parcel = parcelTransactiningTemp;

            }

            return newStation;
        }
        
        /// <summary>
        /// parcel search
        /// </summary>
        /// <returns>found parcel</returns>
        public Parcel Findparcel(int id)//סיימתי
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
                ID = (int)t.ID,
                CustomerName = t.CustomerName,
            };
            IDAL.DO.Drone d = dal.FindDrone((int)p.DroneId);
            Location tempD = new Location()
            {
                Lattitude = d.Lattitude,
                Longitude = d.Longitude,
            };
            DroneInParcel droneInParcelTemp = new()
            {
                ID = (int)d.ID,
                Buttery = d.Buttery,
                current = tempD
            };
            Parcel newParcel = new Parcel()
            {
                ID = (int)p.ID,
                sender = send,
                target = target,
                Weight = (Weight)p.Weight,
                Priority = (Priority)p.Priority,
                Drone = droneInParcelTemp,
                Requested = p.Requested,
                Scheduled = p.Scheduled,
                PickedUp = p.PickedUp,
                Deliverd = p.Deliverd,

            };
            return newParcel;
        }
        
        /// <summary>
        /// customer search
        /// </summary>
        /// <returns>found customer</returns>
        public Customer Findcustomer(int id)//fliping done
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
                ID = (int)c.ID,
                CustomerName = c.CustomerName,
                Phone = c.Phone,
                location = temp,
            };
            List<ParcelInCustomer> TempFromCustomer = new();
            ParcelInCustomer item = new();
            foreach (var item1 in p)
            {
                if (item1.SenderId == id)
                {
                    item.ID = (int)item1.ID;
                    item.priority = (Priority)item1.Priority;
                    item.weight = (Weight)item1.Weight;
                    if (item1.Requested < DateTime.Now && item1.Requested != null)
                    {
                        item.status = (Status)0;
                    }
                    if (item1.Scheduled < DateTime.Now && item1.Scheduled != null)
                    {
                        item.status = (Status)1;
                    }
                    if (item1.PickedUp < DateTime.Now && item1.PickedUp != null)
                    {
                        item.status = (Status)2;
                    }
                    if (item1.Deliverd < DateTime.Now && item1.Deliverd != null)
                    {
                        item.status = (Status)3;
                    }
                    if (item1.Deliverd != null && item1.Scheduled == null)
                    {
                        item.status = (Status)0;
                    }
                    CustomerInParcel q = new()
                    {
                        ID = id,
                        CustomerName = c.CustomerName,
                    };
                    item.sender = q;
                    CustomerInParcel o = new()
                    {
                        ID = item1.TargetId,
                        CustomerName = dal.FindCustomers(item1.TargetId).CustomerName,
                    };
                    item.target = o;
                    TempFromCustomer.Add(item);
                }
            }
            List<ParcelInCustomer> TempToCustomer = new();
            ParcelInCustomer item2 = new();
            foreach (var item3 in p)
            {
                if (item3.TargetId == id)
                {
                    item2.ID = (int)item3.ID;
                    item2.priority = (Priority)item3.Priority;
                    item2.weight = (Weight)item3.Weight;
                    if (item3.Requested < DateTime.Now && item3.Requested != null)
                    {
                        item2.status = (Status)0;
                    }
                    if (item3.Scheduled < DateTime.Now && item3.Scheduled != null)
                    {
                        item2.status = (Status)1;
                    }
                    if (item3.PickedUp < DateTime.Now && item3.PickedUp != null)
                    {
                        item2.status = (Status)2;
                    }
                    if (item3.Deliverd < DateTime.Now && item3.Deliverd != null)
                    {
                        item2.status = (Status)3;
                    }
                    if (item3.Deliverd != null && item3.Scheduled == null)
                    {
                        item2.status = (Status)0;
                    }
                    CustomerInParcel q = new()
                    {
                        ID = id,
                        CustomerName = c.CustomerName,
                    };
                    item2.target = q;
                    CustomerInParcel o = new()
                    {
                        ID = item3.SenderId,
                        CustomerName = dal.FindCustomers(item3.TargetId).CustomerName,
                    };
                    item.sender = o;
                    TempToCustomer.Add(item2);
                }
            }
            newCustomer.fromCustomer = TempFromCustomer;
            newCustomer.toCustomer = TempToCustomer;
            return newCustomer;
        }
        
        //-----------------------------------------------------------------------------------
        //listView func
        //-----------------------------------------------------------------------------------------
        
        /// <summary>
        /// reture all the stations
        /// </summary>
        /// <returns>the stations</returns>
        public IEnumerable<Station> stations()
        {
            List<Station> temp = new();
            foreach (var item in dal.Stationlist())
            {
                temp.Add(FindStation((int)item.ID));
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the drones
        /// </summary>
        /// <returns>the drones</returns>
        public IEnumerable<Drone> Drones()
        {
            List<Drone> temp = new();
            foreach (var item in dal.Dronelist())
            {
                temp.Add(FindDrone((int)item.ID));
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the parcels
        /// </summary>
        /// <returns>the parcels</returns>
        public IEnumerable<Parcel> parcels()
        {
            List<Parcel> temp = new();
            foreach (var item in dal.Parcellist())
            {
                temp.Add(Findparcel((int)item.ID));
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the customers
        /// </summary>
        /// <returns>the customers</returns>
        public IEnumerable<Customer> customers()
        {
            List<Customer> temp = new();
            foreach (var item in dal.Customerlist())
            {
                temp.Add(Findcustomer((int)item.ID));
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the parcels are not associated
        /// </summary>
        /// <returns>the parcels are not associated</returns>
        public IEnumerable<Parcel> parcelsNotAssociated()
        {
            List<Parcel> temp = new();
            foreach (var item in dal.ParcelNotAssociatedList())
            {
                temp.Add(Findparcel((int)item.ID));
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the free chargeslots
        /// </summary>
        /// <returns>the free chargeslots</returns>
        public IEnumerable<Station> freeChargeslots()
        {
            List<Station> temp = new();
            foreach (var item in dal.Stationlist())
            {
                if (item.ChargeSlots > 0)
                {
                    temp.Add(FindStation((int)item.ID));
                }
            }
            return temp;
        }
    }
}