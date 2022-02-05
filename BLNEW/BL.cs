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
        readonly IDal dal = new DalObject();
        /// <summary>
        /// constractor
        /// </summary>
        public BL()
        {
            static int MinPower(Drone drone)/////////////need to be worked on
            {
                return 0;
            }
            IEnumerable<IDAL.DO.Parcel> p = dal.Parcellist();
            Drone tempDrone = new();
            Random random = new();
            foreach (IDAL.DO.Parcel i in p)
            {
                if ((i.Scheduled != null) && (i.Deliverd == null) && (i.DroneId != 0))
                {
                    FindDrone(i.SenderId).Status = (Status)2;
                    if ((i.PickedUp == null) && (i.Scheduled != null))//שויכה אבל לא נאספה
                    {//shortest station
                        Location sta = new()
                        {
                            Lattitude = 0,
                            Longitude = 0
                        };
                        foreach (IDAL.DO.Station item in dal.Stationlist())
                        {
                            if (Distans(FindDrone(i.SenderId).Position, FindDrone((int)i.ID).Position) > Distans(FindDrone((int)i.ID).Position, sta))
                            {
                                sta = FindDrone(i.SenderId).Position;
                            }
                        }
                    }
                    //מפה כל מה שאני אמרתי לך לטפל
                    if ((i.Deliverd == null) && (i.PickedUp != null))
                    {
                        FindDrone(i.SenderId).Position = FindDrone(i.SenderId).Position;//בעיה - צריך להשוות את הרחפן *לשולח
                    }
                    FindDrone(i.SenderId).Battery = random.Next(MinPower(FindDrone(i.SenderId)), 80);//need to check minpower

                }
                if (FindDrone(i.SenderId).Status != (Status)1)//אם הרחפן לא מבצע משלוח
                {
                    FindDrone(i.SenderId).Status = (Status)random.Next(3, 5);
                    if (random.Next(3, 5) == 3)
                    {
                        FindDrone(i.SenderId).Status = Status.CREAT;
                    }
                }
                if (FindDrone(i.SenderId).Status == Status.MAINTENANCE)//
                {
                    List<Station> s = new(); 
                    FindDrone(i.SenderId).Position = FindStation(FreeChargeslots().ToList()[random.Next(0, FreeChargeslots().Count() - 1)].ID).location;
                    FindDrone(i.SenderId).Battery = random.Next(0, 21);
                }
                if (FindDrone(i.SenderId).Status == Status.CREAT)
                {
                    List<Parcel> pa = new();
                    foreach (var item in Parcels())
                    {
                        pa = pa.FindAll(delegate (Parcel p) { return (p.Deliverd != null); });//לקוח שקיבל חבילה
                    }
                    FindDrone(i.SenderId).Position = Findcustomer(pa[random.Next(0, pa.Count - 1)].target.ID).location;//מספר רנדומלי מתוך כל הלקוחות שקיבלו חבילה בו אני מחפש את האיידיי של המקבל שם בחיפוש לקוח ולוקח ממנו את המיקום
                    FindDrone(i.SenderId).Battery = random.Next(MinPower(FindDrone(i.SenderId)), 100);
                }

            }
        }

            /// <summary>
            /// Distans
            /// </summary>
            /// <returns>Distans between a - b</returns>
        static public double Distans(Location a, Location b)
        {
            return Math.Sqrt(Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2));
        }
        
        //add functaions:
        //---------------------------------------------------------------------------------
        
        /// <summary>
        /// Add station
        /// </summary>
        public void AddStation(Station station)
        {
            IDAL.DO.Station tempStation = new()
            {
                ID = station.ID,
                StationName = station.StationName,
                Longitude = station.location.Longitude,
                Lattitude = station.location.Lattitude,
                ChargeSlots = station.FreeChargeSlots,
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
        public void AddDrone(Drone drone, int IDStarting)
        {
            try
            {
                Random random = new();
                IDAL.DO.Drone tempDrone = new()
                {
                    ID = drone.ID,
                    Model = drone.Model,
                    Weight = (IDAL.DO.WEIGHT)drone.Weight,
                    Buttery = drone.Battery,
                    haveParcel = drone.HasParcel,
                };
                IDAL.DO.DroneCharge tempDroneCharge = new()
                {
                    DroneId = drone.ID,
                    StationId =  IDStarting,
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
        public void AddCustomer(Customer customer)
        {
            try
            {
                IDAL.DO.Customer tempCustomer = new()
                {
                    ID = customer.ID,
                    CustomerName = customer.CustomerName,
                    Longitude = customer.location.Longitude,
                    Lattitude = customer.location.Lattitude,
                    Phone = customer.Phone,
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
        public void AddParcel(Parcel parcel)
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
                IDAL.DO.Parcel tempParcel = new()
                {
                    SenderId = parcel.sender.ID,
                    TargetId = parcel.target.ID,
                    Weight = (IDAL.DO.WEIGHT)parcel.Weight,
                    Priority = (IDAL.DO.PRIORITY)parcel.Priority,
                    Requested = parcel.Requested,
                    Scheduled = parcel.Scheduled,
                    PickedUp = parcel.PickedUp,
                    Deliverd = parcel.Deliverd,
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
        public void UpdateDrone(Drone drone)
        {
            IDAL.DO.Drone d = dal.FindDrone(drone.ID);
            d.Model = drone.Model;
            foreach (var item in DataSource.drones)
            {
                if (item.ID == drone.ID)
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
        public void UpdateStation(Station station)
        {
            IDAL.DO.Station s = dal.FindStation(station.ID);
            s.StationName = station.StationName;
            s.ChargeSlots = station.FreeChargeSlots;
            foreach (var item in DataSource.stations)
            {
                if (item.ID == station.ID)
                {
                    DataSource.stations.Remove(item);
                }
            }
            dal.AddStation(s);
        }
        
        /// <summary>
        /// Update Custemer details
        /// </summary>
        public void UpdateCustomer(Customer customer)
        {
            IDAL.DO.Customer c = dal.FindCustomers(customer.ID);
            c.CustomerName = customer.CustomerName;
            c.Phone = customer.Phone;
            foreach (var item in DataSource.customers)
            {
                if (item.ID == customer.ID)
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
                foreach (var item in Stations())
                {
                    if (Distans(FindStation(item.ID).location, FindDrone(id).Position) > distans)
                    {
                        distans = Distans(FindStation(item.ID).location, FindDrone(id).Position);
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
                List<ParcelToList> temp = ParcelsNotAssociated().ToList();
                List<ParcelToList> temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.SOS; });
                if (temp1.Count == 0)
                {
                    temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.FAST; });
                    if (temp1.Count == 0)
                    {
                        temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.REGULAR; });
                        if (temp1.Count == 0)
                        {
                            throw new ThereIsNoParcel("there are no parcel");
                        }
                    }
                }
                temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.SOS; });
                if (temp1.Count == 0)
                {
                    temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.FAST; });
                    if (temp1.Count == 0)
                    {
                        temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.REGULAR; });
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
                    if (Distans(FindDrone(id).Position, Findcustomer(Findparcel(item.ID).sender.ID).location) > Distans(FindDrone(id).Position, location))
                    {
                        location.Lattitude = Findcustomer(Findparcel(item.ID).sender.ID).location.Lattitude;
                        location.Longitude = Findcustomer(Findparcel(item.ID).sender.ID).location.Longitude;
                        saveID = item.ID;
                    }
                }
                Findparcel(saveID).Drone.ID = id;
                Findparcel(saveID).Drone.Buttery = FindDrone(id).Battery;
                Findparcel(saveID).Drone.Position = FindDrone(id).Position;
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
            Location temp = new()
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
            Station newStation = new()
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
            Drone newStation = new();
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
            Location tempD = new()
            {
                Lattitude = d.Lattitude,
                Longitude = d.Longitude,
            };
            DroneInParcel droneInParcelTemp = new()
            {
                ID = (int)d.ID,
                Buttery = d.Buttery,
                Position = tempD
            };
            Parcel newParcel = new()
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
            Location temp = new()
            {
                Lattitude = c.Lattitude,
                Longitude = c.Longitude,
            };
            Customer newCustomer = new()
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
                    item.Priority = (Priority)item1.Priority;
                    item.Weight = (Weight)item1.Weight;
                    if (item1.Requested < DateTime.Now && item1.Requested != null)
                    {
                        item.Status = (Status)0;
                    }
                    if (item1.Scheduled < DateTime.Now && item1.Scheduled != null)
                    {
                        item.Status = (Status)1;
                    }
                    if (item1.PickedUp < DateTime.Now && item1.PickedUp != null)
                    {
                        item.Status = (Status)2;
                    }
                    if (item1.Deliverd < DateTime.Now && item1.Deliverd != null)
                    {
                        item.Status = (Status)3;
                    }
                    if (item1.Deliverd != null && item1.Scheduled == null)
                    {
                        item.Status = (Status)0;
                    }
                    CustomerInParcel q = new()
                    {
                        ID = id,
                        CustomerName = c.CustomerName,
                    };
                    item.Sender = q;
                    CustomerInParcel o = new()
                    {
                        ID = item1.TargetId,
                        CustomerName = dal.FindCustomers(item1.TargetId).CustomerName,
                    };
                    item.Target = o;
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
                    item2.Priority = (Priority)item3.Priority;
                    item2.Weight = (Weight)item3.Weight;
                    if (item3.Requested < DateTime.Now && item3.Requested != null)
                    {
                        item2.Status = (Status)0;
                    }
                    if (item3.Scheduled < DateTime.Now && item3.Scheduled != null)
                    {
                        item2.Status = (Status)1;
                    }
                    if (item3.PickedUp < DateTime.Now && item3.PickedUp != null)
                    {
                        item2.Status = (Status)2;
                    }
                    if (item3.Deliverd < DateTime.Now && item3.Deliverd != null)
                    {
                        item2.Status = (Status)3;
                    }
                    if (item3.Deliverd != null && item3.Scheduled == null)
                    {
                        item2.Status = (Status)0;
                    }
                    CustomerInParcel q = new()
                    {
                        ID = id,
                        CustomerName = c.CustomerName,
                    };
                    item2.Target = q;
                    CustomerInParcel o = new()
                    {
                        ID = item3.SenderId,
                        CustomerName = dal.FindCustomers(item3.TargetId).CustomerName,
                    };
                    item.Sender = o;
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
        public IEnumerable<StationToList> Stations()
        {
            List<StationToList> temp = new();
            List<Station> stations = new();
            foreach (var item in dal.Stationlist())
            {
                stations.Add(FindStation((int)item.ID));
            }
            for (int i = 0; i < stations.Count; i++)
            {
                temp[i].ID = stations[i].ID;
                temp[i].StationName = stations[i].StationName;
                temp[i].FreeChargeSlots = stations[i].FreeChargeSlots;
                temp[i].UsedChargeSlots = 5-stations[i].FreeChargeSlots;//חייבים לבדוק כל הזמן שזה לא שלילי אם זה שלילי חייבים לברר מה ההבעיה
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the drones
        /// </summary>
        /// <returns>the drones</returns>
        public IEnumerable<DroneToList> Drones()
        {
            
            List<Drone> drones = new();

            foreach (var item in dal.Dronelist())
            {
                drones.Add(FindDrone((int)item.ID));
            }
            List<DroneToList> temp = new(drones.Count);
            for (int i = 0; i < drones.Count; i++)
            {
                DroneToList droneToList = new() { };
                droneToList.ID = drones[i].ID;
                try
                {
                    droneToList.IdParcel = drones[i].Parcel.ID;
                }
                catch(Exception ex)
                {
                    if (ex.Message == "NullReferenceException")
                        droneToList.IdParcel = null;
                }
                droneToList.Model = drones[i].Model;
                droneToList.Status = drones[i].Status;
                droneToList.Weight = drones[i].Weight;
                droneToList.Buttery = drones[i].Battery;
                droneToList.Position = drones[i].Position;
                
                Console.WriteLine(droneToList);
                temp.Add(droneToList);
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the parcels
        /// </summary>
        /// <returns>the parcels</returns>
        public IEnumerable<ParcelToList> Parcels()
        {
            List<Parcel> parcels = new();
            List<ParcelToList> temp = new();

            foreach (var item in dal.Parcellist())
            {
                parcels.Add(Findparcel((int)item.ID));
            }
            for (int i = 0; i < parcels.Count; i++)
            {
                temp[i].ID = parcels[i].ID;
                temp[i].Priority = parcels[i].Priority;
                temp[i].SenderName = parcels[i].sender.CustomerName;
                if (parcels[i].Requested < DateTime.Now && parcels[i].Requested != null)
                {
                    temp[i].status = (Status)0;
                }
                if (parcels[i].Scheduled < DateTime.Now && parcels[i].Scheduled != null)
                {
                    temp[i].status = (Status)1;
                }
                if (parcels[i].PickedUp < DateTime.Now && parcels[i].PickedUp != null)
                {
                    temp[i].status = (Status)2;
                }
                if (parcels[i].Deliverd < DateTime.Now && parcels[i].Deliverd != null)
                {
                    temp[i].status = (Status)3;
                }
                if (parcels[i].Deliverd != null && parcels[i].Scheduled == null)
                {
                    temp[i].status = (Status)0;
                }
                temp[i].TargetName = parcels[i].target.CustomerName;
                temp[i].Weight = parcels[i].Weight;
            }
            return temp;
        }

        /// <summary>
        /// reture all the customers
        /// </summary>
        /// <returns>the customers</returns>
        public IEnumerable<CustomerToList> Customers()
        {
            List<CustomerToList> temp = new();
            List<Customer> customer = new();
            int counter1 = 0, counter2 = 0;
            foreach (var item in dal.Dronelist())
            {
                customer.Add(Findcustomer((int)item.ID));
            }
            for (int i = 0; i < customer.Count; i++)
            {
                temp[i].ID = customer[i].ID;
                temp[i].CustomerName = customer[i].CustomerName;
                foreach (var item in customer[i].toCustomer)
                {
                    if (item.Status != Status.PROVID)
                        counter1++;
                    else
                        counter2++;
                }
                temp[i].NumFoParcelOnWay = counter1;
                counter1 = 0;
                temp[i].NumFoParcelReceived = counter2;
                counter2 = 0;
                foreach (var item in customer[i].fromCustomer)
                {
                    if (item.Status != Status.PROVID)
                        counter1++;
                    else
                        counter2++;
                }
                temp[i].NumFoParcelSent = counter1;
                temp[i].NumFoParcelSentAndDelivered = counter2;
                temp[i].Phone = customer[i].Phone;
            }
            return temp;
        }
        /// <summary>
        /// reture all the parcels are not associated
        /// </summary>
        /// <returns>the parcels are not associated</returns>
        public IEnumerable<ParcelToList> ParcelsNotAssociated()
        {
            List<Parcel> parcels = new();
            List<ParcelToList> temp = new();

            foreach (var item in dal.ParcelNotAssociatedList())
            {
                parcels.Add(Findparcel((int)item.ID));
            }
            for (int i = 0; i < parcels.Count; i++)
            {
                temp[i].ID = parcels[i].ID;
                temp[i].Priority = parcels[i].Priority;
                temp[i].SenderName = parcels[i].sender.CustomerName;
                if (parcels[i].Requested < DateTime.Now && parcels[i].Requested != null)
                {
                    temp[i].status = (Status)0;
                }
                if (parcels[i].Scheduled < DateTime.Now && parcels[i].Scheduled != null)
                {
                    temp[i].status = (Status)1;
                }
                if (parcels[i].PickedUp < DateTime.Now && parcels[i].PickedUp != null)
                {
                    temp[i].status = (Status)2;
                }
                if (parcels[i].Deliverd < DateTime.Now && parcels[i].Deliverd != null)
                {
                    temp[i].status = (Status)3;
                }
                if (parcels[i].Deliverd != null && parcels[i].Scheduled == null)
                {
                    temp[i].status = (Status)0;
                }
                temp[i].TargetName = parcels[i].target.CustomerName;
                temp[i].Weight = parcels[i].Weight;
            }
            return temp;
        }
        
        /// <summary>
        /// reture all the free chargeslots
        /// </summary>
        /// <returns>the free chargeslots</returns>
        public IEnumerable<StationToList> FreeChargeslots()
        {
            List<StationToList> temp = new();
            List<Station> stations = new();
            foreach (var item in dal.Stationlist())
            {
                if (item.ChargeSlots > 0)
                    stations.Add(FindStation((int)item.ID));
            }
            for (int i = 0; i < stations.Count; i++)
            {
                temp[i].ID = stations[i].ID;
                temp[i].StationName = stations[i].StationName;
                temp[i].FreeChargeSlots = stations[i].FreeChargeSlots;
                temp[i].UsedChargeSlots = 5 - stations[i].FreeChargeSlots;//חייבים לבדוק כל הזמן שזה לא שלילי אם זה שלילי חייבים לברר מה ההבעיה
            }
            return temp;
        }
    }
}