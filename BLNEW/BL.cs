using BLExceptions;
using BO;
using DAL;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;
using System.Runtime.CompilerServices;

namespace BL
{
    public sealed class BL : BlApi.IBL
    {
        internal IDal dal = DalFactory.GetDal("DalXml");
        /// <summary>
        /// contractor
        /// </summary> 

        private BL()
        {

            DO.Drone tempDrone = new();
            lock (dal)
            {
                IEnumerable<DO.Parcel> p = dal.Parcellist();
                Random random = new();
                foreach (DO.Parcel i in p)
                {
                    if ((i.Status != DO.StatusParcel.DELIVERD) && (i.DroneId != 0))
                    {
                        tempDrone = dal.FindDrone((int)i.DroneId);
                        
                        tempDrone.Battery = random.Next(MinPower(FindDrone(i.DroneId), Findcustomer(i.TargetId), Findcustomer(i.SenderId)), 100);
                        if ((i.PickedUp == null) && (i.Scheduled != null))//belong not pickup
                        {//shortest station
                            tempDrone.Status = DO.Status.MAINTENANCE;
                            dal.UpdateDrone(tempDrone);
                            Location sta = new()
                            {
                                Lattitude = 0,
                                Longitude = 0,
                            };
                            double d = 0;
                            foreach (var item in FreeChargeslots())
                            {
                                if (Distance(FindStation(item.ID).Position, Findcustomer(i.SenderId).Position) > d)
                                {
                                    d = Distance(FindStation(item.ID).Position, Findcustomer(i.SenderId).Position);
                                    sta = FindStation(item.ID).Position;
                                }
                            }
                            dal.DroneOutCharge(i.DroneId);
                            tempDrone.Status = DO.Status.BELONG;
                            tempDrone.Lattitude = sta.Lattitude;
                            tempDrone.Longitude = sta.Longitude;
                        }

                        if ((i.Scheduled != null) && (i.Deliverd == null) && (i.PickedUp != null))//pickup not delivered
                        {
                            tempDrone.Status = DO.Status.PICKUP;
                            tempDrone.Longitude = dal.FindCustomers(i.SenderId).Longitude;
                            tempDrone.Lattitude = dal.FindCustomers(i.SenderId).Lattitude;
                        }
                        dal.UpdateDrone(tempDrone);
                    }
                }
                foreach (var item in dal.Dronelist())
                {
                    if (item.Status == DO.Status.CREAT)
                    {
                        tempDrone.Status = DO.Status.MAINTENANCE;
                        dal.UpdateDrone(tempDrone);
                        dal.DroneOutCharge((int)item.ID);
                        tempDrone = dal.FindDrone((int)item.ID);
                        tempDrone.Status = DO.Status.CREAT;
                        int temp = random.Next(0, 2);
                        if (temp == 0)
                        {
                            List<DO.Parcel> pa = new();
                            pa = dal.Parcellist().ToList().FindAll(delegate (DO.Parcel p) { return p.Deliverd != null; });//Customer who received a package
                            if (pa.Count == 0)
                            {
                                tempDrone.Lattitude = dal.FindCustomers(Customers().ToList()[random.Next(0, Customers().Count() - 1)].ID).Lattitude;
                                tempDrone.Longitude = dal.FindCustomers(Customers().ToList()[random.Next(0, Customers().Count() - 1)].ID).Longitude;
                            }
                            else
                            {
                                int q = random.Next(0, pa.Count - 1);
                                tempDrone.Lattitude = dal.FindCustomers(pa[q].TargetId).Lattitude;
                                tempDrone.Longitude = dal.FindCustomers(pa[q].TargetId).Longitude;
                            }
                            tempDrone.Battery = 80;
                            dal.UpdateDrone(tempDrone);
                        }
                        else
                        {
                            tempDrone.Battery = 21;
                            dal.UpdateDrone(tempDrone);
                            DroneToCharge((int)tempDrone.ID);
                        }
                    }
                }
            }
        }
        static BL instance = null;
        public static BL GetInstance()
        {
            if (instance == null)
                instance = new BL();
            return instance;
        }
        int MinPower(Drone drone,Customer Target, Customer Sender)
        {
            double a = 0;
            int c = 0;
            int? StationID;
            foreach (var item in dal.Stationlist())
            {
                Location location = new()
                {
                    Lattitude = item.Lattitude,
                    Longitude = item.Longitude,
                };

                if ((a > Distance(location, Target.Position)) && c != 0)
                {
                    a = Distance(location, Target.Position);
                    StationID = item.ID;
                }
                if (c == 0)
                {
                    a = Distance(location, Target.Position);
                    c++;
                }
            }
            lock (dal)
            {
                double i = dal.Power()[((int)drone.Weight + 1) % 4];

                
                a += Distance(Sender.Position, Target.Position);
                i *= a;
                i = Math.Ceiling(i);
                return (int)i;
            }
        }
        /// <summary>
        /// Distance
        /// </summary>
        /// <returns>Distance between a - b</returns>
        static private double Distance(Location a, Location b)
        {
            return Math.Sqrt(Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2));
        }
        //add functions:
        //---------------------------------------------------------------------------------
        /// <summary>
        /// Add station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station station)
        {
            try
            {
                DO.Station tempStation = new()
                {
                    IsActive = true,
                    ID = station.ID,
                    StationName = station.StationName,
                    Longitude = station.Position.Longitude,
                    Lattitude = station.Position.Lattitude,
                    ChargeSlots = station.ChargeSlots,
                };
                lock (dal)
                {
                    dal.AddStation(tempStation);
                }
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException($"{ex.Message}");
            }
        }

        /// <summary>
        /// Add drone
        /// </summary>
        readonly Random ran = new();
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone, int IDStarting)
        {
            try
            {
                DO.Drone tempDrone = new()
                {
                    IsActive = true,
                    ID = drone.ID,
                    Model = (DO.Model)drone.Model,
                    Weight = (DO.Weight)drone.Weight,
                    Battery = ran.Next(20, 40),
                    haveParcel = false,
                };
                lock (dal)
                {
                    Station s = FindStation(IDStarting);
                    tempDrone.Lattitude = s.Position.Lattitude;
                    tempDrone.Longitude = s.Position.Longitude;
                    DroneCharging temp = new() { ID = (int)drone.ID, Battery = tempDrone.Battery };
                    s.DroneChargingInStation.Add(temp);
                    UpdateStation(s);
                    dal.AddDroneCharge((int)drone.ID, IDStarting);
                    dal.AddDrone(tempDrone);
                }
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }

        }

        /// <summary>
        /// Add customer
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            try
            {
                DO.Customer tempCustomer = new()
                {
                    IsActive = true,
                    ID = customer.ID,
                    CustomerName = customer.CustomerName,
                    Longitude = customer.Position.Longitude,
                    Lattitude = customer.Position.Lattitude,
                    Phone = customer.Phone,
                };
                lock (dal)
                {
                    dal.AddCustomer(tempCustomer);
                }
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Add parcel
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel parcel)
        {
            try
            {
                DO.Parcel tempParcel = new()
                {
                    IsActive = true,
                    ID = parcel.ID,
                    Status = DO.StatusParcel.CREAT,
                    SenderId = parcel.sender.ID,
                    TargetId = parcel.target.ID,
                    Weight = (DO.Weight)parcel.Weight,
                    Priority = (DO.Priority)parcel.Priority,
                    Requested = DateTime.Now,
                    Scheduled = null,
                    PickedUp = null,
                    Deliverd = null,
                    DroneId = 0,
                };
                lock (dal)
                {
                    dal.AddParcel(tempParcel);
                }
            }
            catch (Exception ex)
            {
                throw new AlreadyExistException(ex.Message, ex);
            }
        }

        //---------------------------------------------------------------------------------
        //updating functions:
        //---------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel parcel) => dal.UpdateParcel(new DO.Parcel
        {
            IsActive = parcel.IsActive,
            ID = parcel.ID,
            Weight = (DO.Weight)parcel.Weight,
            Requested = parcel.Requested,
            Deliverd = parcel.Deliverd,
            Scheduled = parcel.Scheduled,
            SenderId = parcel.sender.ID,
            DroneId = parcel.Drone.ID,
            PickedUp = parcel.PickedUp,
            Priority = (DO.Priority)parcel.Priority,
            TargetId = parcel.target.ID,
        });

        /// <summary>
        /// Update Drone model
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone drone)
        {
            lock (dal)
            {
                dal.UpdateDrone(new DO.Drone
                {
                    IsActive = drone.IsActive,
                    ID = drone.ID,
                    Battery = drone.Battery,
                    haveParcel = drone.HaveParcel,
                    Lattitude = drone.Position.Lattitude,
                    Longitude = drone.Position.Longitude,
                    Model = (DO.Model)drone.Model,
                    Status = (DO.Status)drone.Status,
                    Weight = (DO.Weight)drone.Weight,
                });
            }
        }
        /// <summary>
        /// Update Station details
        /// </summary>
#nullable enable
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station station)
        {
            lock (dal)
            {
                dal.UpdateStation(new DO.Station
                {
                    IsActive = station.IsActive,
                    ID = station.ID,
                    StationName = station.StationName,
                    ChargeSlots = station.ChargeSlots,
                    Lattitude = station.Position.Lattitude,
                    Longitude = station.Position.Longitude,
                    BusyChargeSlots = station.DroneChargingInStation.Count,
                });
            }
        }

        /// <summary>
        /// Update Customer details
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            lock (dal)
            {
                dal.UpdateCustomer(new DO.Customer
                {
                    IsActive = customer.IsActive,
                    ID = customer.ID,
                    CustomerName = customer.CustomerName,
                    Lattitude = customer.Position.Lattitude,
                    Longitude = customer.Position.Longitude,
                    Phone = customer.Phone,
                });
            }
        }
#nullable disable

        /// <summary>
        /// Inserting a drone from a charger
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneToCharge(int id)
        {
            lock (dal)
            {

                DO.Drone d = dal.FindDrone(id);
                if (d.Status == DO.Status.BELONG || d.Status == DO.Status.PICKUP)
                    throw new DroneInActionException($"the drone {id} is in the medal of an action");
                else if (d.Battery < 20)
                    throw new DontHaveEnoughPowerException($"the drone {id} don't have enough power");
                else if (d.Status == DO.Status.MAINTENANCE)
                    throw new DroneIsAlreadyChargeException($"the drone {id} already charge");
                else
                {
                    int StationID = Stations().OrderBy(i => Distance(FindStation(i.ID).Position, FindDrone(id).Position)).First().ID;
                    dal.DroneToCharge(id, StationID);
                }
            }
        }
        /// <summary>
        /// Removing a drone from a charger
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneOutCharge(int id, double time)
        {
            lock (dal)
            {
                if (FindDrone(id).Status == Status.MAINTENANCE)
                {
                    Drone drone = FindDrone(id);
                    drone.Status = Status.CREAT;
                    drone.Battery = dal.Power()[4] * (time / 60);
                    var temp = dal.DroneChargelist().Where(i => i.DroneId == id).ToList();
                    Station station = FindStation(temp[0].StationId);
                    int index = station.DroneChargingInStation.FindIndex(i => i.ID == id);
                    station.DroneChargingInStation.RemoveAt(index);
                    UpdateDrone(drone);
                    dal.DroneOutCharge(id);
                }
                else
                {
                    throw new DroneDontInChargingException($"The Drone {id} Doesn't In Charging");
                }
            }
        }

        /// <summary>
        /// Assign a parcel to a drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AttacheDrone(int id)
        {
            lock (dal)
            {
                if (!FindDrone(id).HaveParcel)
                {
                    if (!ParcelsNotAssociated().Any())
                        throw new ThereIsNoParcelToAttachdException("There is no parcel to attached");
                    var parcel = ParcelsNotAssociated().OrderByDescending(i => i.Priority).OrderByDescending(i=> i.Weight).First();
                    dal.AttacheDrone(parcel.ID);
                }
                else
                    throw new DroneIsBusyException($"the drone {id} in busy right new");
            }
        }

        /// <summary>
        /// Collection of a parcel by drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpParcel(int id)
        {
            try
            {
                lock (dal)
                {

                    int t = (int)FindDrone(id).Parcel.ID;
                    if (Findparcel(t).PickedUp != null)
                        throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have picked up");
                    else
                        dal.PickupParcel(t);
                }
            }
            catch (Exception) { throw new ParcelPastErroeException($"there are no parcel to pickup"); }
        }

        /// <summary>
        /// Delivery of a parcel by drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Parceldelivery(int id)
        {
            try
            {
                lock (dal)
                {
                    int t = (int)FindDrone(id).Parcel.ID;
                    if (Findparcel(t).Deliverd != null)
                        throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have delivered up");
                    else
                        dal.DeliverdParcel(t);
                }
            }
            catch (Exception) { throw new ParcelPastErroeException($"there are no parcel to delivered"); }
        }

        //-----------------------------------------------------------------------------
        //display func
        //------------------------------------------------------------------------------
        /// <summary>
        /// station search
        /// </summary>
        /// <returns>found station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station FindStation(int id)
        {
            lock (dal)
            {
                DO.Station s = dal.FindStation(id);
                List<DroneCharging> droneChargingTemp = new();
                var droneChargingfiltered = dal.DroneChargelist().Where(i => i.StationId == id).ToList();
                droneChargingfiltered.ForEach(i => droneChargingTemp.Add(new() { ID = (int)i.DroneId, Battery = FindDrone((int)i.DroneId).Battery }));
                Location temp = new()
                {
                    Lattitude = s.Lattitude,
                    Longitude = s.Longitude,
                };
                Station newStation = new()
                {
                    IsActive = s.IsActive,
                    ID = (int)s.ID,
                    StationName = s.StationName,
                    Position = temp,
                    ChargeSlots = s.ChargeSlots,
                    DroneChargingInStation = droneChargingTemp,
                };
                return newStation;
            }
        }

        /// <summary>
        /// drone search
        /// </summary>
        /// <returns>found drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone FindDrone(int id)
        {
            lock (dal)
            {
                DO.Drone d = dal.FindDrone(id);
                ParcelTransactioning parcelTransactiningTemp = new();
                parcelTransactiningTemp.ID = null;
                Drone newDrone = new();
                newDrone.IsActive = d.IsActive;
                newDrone.HaveParcel = d.haveParcel;
                newDrone.ID = d.ID;
                newDrone.Model = (Model)d.Model;
                newDrone.Weight = (Weight)d.Weight;
                newDrone.Status = (Status)d.Status;
                newDrone.Battery = d.Battery;
                Location locationDrone = new()
                {
                    Lattitude = d.Lattitude,
                    Longitude = d.Longitude,
                };
                newDrone.Position = locationDrone;

                if (d.Status == DO.Status.PICKUP || d.Status == DO.Status.BELONG)
                {
                    var p = dal.Parcellist().Where(i => i.DroneId == id).First();
                    DO.Customer s = dal.FindCustomers(p.SenderId);
                    DO.Customer t = dal.FindCustomers(p.TargetId);
                    CustomerInParcel Sender = new()
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
                    parcelTransactiningTemp.sender = Sender;
                    parcelTransactiningTemp.target = target;
                    parcelTransactiningTemp.LocationOfSender = locationSend;
                    parcelTransactiningTemp.LocationOftarget = locationTarget;
                    parcelTransactiningTemp.distance = Distance(locationSend, locationTarget);


                }
                newDrone.Parcel = parcelTransactiningTemp;
                return newDrone;
            }
        }

        /// <summary>
        /// parcel search
        /// </summary>
        /// <returns>found parcel</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel Findparcel(int id)
        {
            lock (dal)
            {
                DO.Parcel p = dal.FindParcel(id);
                DO.Customer s = dal.FindCustomers(p.SenderId);
                DO.Customer t = dal.FindCustomers(p.TargetId);
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
                Parcel newParcel = new()
                {
                    IsActive = p.IsActive,
                    ID = (int)p.ID,
                    sender = send,
                    target = target,
                    Weight = (Weight)p.Weight,
                    Priority = (Priority)p.Priority,
                    Requested = p.Requested,
                    Scheduled = p.Scheduled,
                    PickedUp = p.PickedUp,
                    Deliverd = p.Deliverd,

                };
                DroneInParcel droneInParcelTemp = new()
                {
                    ID = 0,
                };
                if (p.DroneId != 0)
                {
                    DO.Drone d = dal.FindDrone(p.DroneId);
                    Location temp = new()
                    {
                        Lattitude = d.Lattitude,
                        Longitude = d.Longitude,
                    };
                    droneInParcelTemp.ID = (int)d.ID;
                    droneInParcelTemp.Battery = d.Battery;
                    droneInParcelTemp.Position = temp;
                    newParcel.Drone = droneInParcelTemp;

                }
                return newParcel;
            }
        }

        /// <summary>
        /// customer search
        /// </summary>
        /// <returns>found customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer Findcustomer(int id)
        {
            lock (dal)
            {
                DO.Customer c = dal.FindCustomers(id);
                IEnumerable<DO.Parcel> p = dal.Parcellist();
                Location temp = new()
                {
                    Lattitude = c.Lattitude,
                    Longitude = c.Longitude,
                };
                Customer newCustomer = new()
                {
                    IsActive = c.IsActive,
                    ID = (int)c.ID,
                    CustomerName = c.CustomerName,
                    Phone = c.Phone,
                    Position = temp,
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
                        item.Status = (StatusParcel)item1.Status;
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
                        item2.Status = (StatusParcel)item3.Status;
                        CustomerInParcel q = new()
                        {
                            ID = id,
                            CustomerName = c.CustomerName,
                        };
                        item2.Target = q;
                        CustomerInParcel o = new()
                        {
                            ID = item3.SenderId,
                            CustomerName = dal.FindCustomers(item3.SenderId).CustomerName,
                        };
                        item2.Sender = o;
                        TempToCustomer.Add(item2);
                    }
                }
                newCustomer.fromCustomer = TempFromCustomer;
                newCustomer.toCustomer = TempToCustomer;
                return newCustomer;
            }
        }

        //-----------------------------------------------------------------------------------
        //listView func
        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// return all the stations
        /// </summary>
        /// <returns>the stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> Stations()
        {
            lock (dal)
            {
                return from s in dal.Stationlist()
                       select new StationToList()
                       {
                           ID = (int)s.ID,
                           StationName = s.StationName,
                           FreeChargeSlots = s.ChargeSlots - s.BusyChargeSlots,
                           UsedChargeSlots = s.BusyChargeSlots
                       };
            }
        }

        /// <summary>
        /// return all the drones
        /// </summary>
        /// <returns>the drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneToList> Drones()
        {
            lock (dal)
            {
                return from d in dal.Dronelist()
                       select new DroneToList()
                       {
                           ID = (int)d.ID,
                           Battery = d.Battery,
                           IdParcel = FindDrone((int)d.ID).Parcel.ID,
                           Model = (Model)d.Model,
                           Position = FindDrone((int)d.ID).Position,
                           Status = (Status)d.Status,
                           Weight = (Weight)d.Weight,
                       };
            }
        }

        /// <summary>
        /// return all the parcels
        /// </summary>
        /// <returns>the parcels</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> Parcels()
        {
            lock (dal)
            {
                return from p in dal.Parcellist()
                       select new ParcelToList()
                       {
                           ID = (int)p.ID,
                           Priority = (Priority)p.Priority,
                           status = (Status)p.Status,
                           SenderName = Findparcel((int)p.ID).sender.CustomerName,
                           TargetName = Findparcel((int)p.ID).target.CustomerName,
                       };
            }
        }

        /// <summary>
        /// return all the customers
        /// </summary>
        /// <returns>the customers</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> Customers()
        {
            lock (dal)
            {
                return from c in dal.Customerlist()
                       select new CustomerToList()
                       {
                           ID = (int)c.ID,
                           CustomerName = c.CustomerName,
                           Phone = c.Phone,
                           NumFoParcelSent = Findcustomer((int)c.ID).fromCustomer.Count,
                           NumFoParcelOnWay = Findcustomer((int)c.ID).toCustomer.Count,
                           NumFoParcelReceived = Findcustomer((int)c.ID).toCustomer.Count(i => i.Status == StatusParcel.DELIVERD),
                           NumFoParcelSentAndDelivered = Findcustomer((int)c.ID).fromCustomer.Count(i => i.Status == StatusParcel.DELIVERD),
                       };
            }

        }
        /// <summary>
        /// return all the parcels are not associated
        /// </summary>
        /// <returns>the parcels are not associated</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ParcelsNotAssociated()
        {
            lock (dal)
            {
                return from p in dal.ParcelNotAssociatedList()
                       select new ParcelToList()
                       {
                           ID = (int)p.ID,
                           Priority = (Priority)p.Priority,
                           status = (Status)p.Status,
                           SenderName = Findparcel((int)p.ID).sender.CustomerName,
                           TargetName = Findparcel((int)p.ID).target.CustomerName,
                       };
            }
        }

        /// <summary>
        /// return all the free chargeslots
        /// </summary>
        /// <returns>the free chargeslots</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationToList> FreeChargeslots()
        {
            lock (dal)
            {
                return from s in dal.Freechargeslotslist()
                       select new StationToList()
                       {
                           ID = (int)s.ID,
                           StationName = s.StationName,
                           FreeChargeSlots = s.ChargeSlots - s.BusyChargeSlots,
                           UsedChargeSlots = s.BusyChargeSlots
                       };
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel parcel)
        {
            lock (dal)
            {
                if (parcel.Scheduled == null)
                    dal.DeleteParcel(dal.FindParcel(parcel.ID));
                else
                    throw new CantDeleteException($"you can't delete this parcel: {parcel.ID}");
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station station)
        {
            lock (dal)
                dal.DeleteStation(dal.FindStation(station.ID));
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer customer)
        {
            lock (dal)
                dal.DeleteCustomer(dal.FindCustomers(customer.ID));
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone drone)
        {
            lock (dal)
            {
                if (drone.Status == Status.MAINTENANCE && drone.Status == Status.CREAT)
                    dal.DeleteDrone(dal.FindDrone((int)drone.ID));
                else
                    throw new CantDeleteException($"you can't delete this drone: {drone.ID}");
            }
        }

        public void Uploader(int droneId, Action display, Func<bool> checker)
        {
            throw new NotImplementedException();
        }
    }
}