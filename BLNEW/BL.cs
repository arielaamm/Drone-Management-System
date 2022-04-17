using BLExceptions;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DateTime = System.DateTime;

namespace BL
{
    /// <summary>
    /// Defines the <see cref="BL" />.
    /// </summary>
    public sealed class BL : BlApi.IBL
    {
        /// <summary>
        /// Defines the dal.
        /// </summary>
        internal IDal dal = DalFactory.GetDal("DalXml");

        /// <summary>
        /// Prevents a default instance of the <see cref="BL"/> class from being created.
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
                        tempDrone = dal.FindDrone(i.DroneId);

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
                    if (item.Status == DO.Status.FREE)
                    {
                        tempDrone.Status = DO.Status.MAINTENANCE;
                        dal.UpdateDrone(tempDrone);
                        dal.DroneOutCharge((int)item.ID);
                        tempDrone = dal.FindDrone((int)item.ID);
                        tempDrone.Status = DO.Status.FREE;
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
                            tempDrone.Battery = 80;
                            dal.UpdateDrone(tempDrone);
                            DroneToCharge((int)tempDrone.ID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Defines the instance.
        /// </summary>
        public static BL instance = null;

        /// <summary>
        /// The GetInstance.
        /// </summary>
        /// <returns>The <see cref="BL"/>.</returns>
        public static BL GetInstance()
        {
            if (instance == null)
                instance = new BL();
            return instance;
        }

        /// <summary>
        /// The MinPower.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        /// <param name="Target">The Target<see cref="Customer"/>.</param>
        /// <param name="Sender">The Sender<see cref="Customer"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int MinPower(Drone drone, Customer Target, Customer Sender)
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
        public static double PowerConsumption(double distance, Weight a)
        {
            if (a == Weight.FREE)
                return (5 * distance);
            if (a == Weight.LIGHT)
                return (7 * distance);
            if (a == Weight.MEDIUM)
                return (10 * distance);
            return (12 * distance);
        }
        /// <summary>
        /// Distance.
        /// </summary>
        /// <param name="a">The a<see cref="Location"/>.</param>
        /// <param name="b">The b<see cref="Location"/>.</param>
        /// <returns>Distance between a - b.</returns>
        public static double Distance(Location a, Location b)
        {
            return Math.Sqrt(Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2));
        }
        /// <summary>
        /// Add station.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
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
        /// Add drone.
        /// </summary>
        public readonly Random ran = new();

        /// <summary>
        /// The AddDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        /// <param name="IDStarting">The IDStarting<see cref="int"/>.</param>
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
                    Status = DO.Status.MAINTENANCE,
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
        /// Add customer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
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
                    Email = customer.Email,
                    Password = customer.Password,
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
        /// Add parcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
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
        /// <summary>
        /// The UpdateParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
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
        /// Update Drone model.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
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
        /// Update Station details.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
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
        /// Update Customer details.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
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
                    Email = customer.Email,
                    Password= customer.Password,
                });
            }
        }

        /// <summary>
        /// Inserting a drone from a charger.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
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
        /// Removing a drone from a charger.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="time">The time<see cref="double"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneOutCharge(int id, double time)
        {
            lock (dal)
            {
                if (FindDrone(id).Status == Status.MAINTENANCE)
                {
                    Drone drone = FindDrone(id);
                    var temp = dal.DroneChargelist().Where(i => i.DroneId == id).FirstOrDefault();
                    Station station = FindStation(temp.StationId);
                    int index = station.DroneChargingInStation.FindIndex(i => i.ID == id);
                    station.DroneChargingInStation.RemoveAt(index);
                    UpdateDrone(drone);
                    dal.DroneOutCharge(id, time);
                }
                else
                {
                    throw new DroneDontInChargingException($"The Drone {id} Doesn't In Charging");
                }
            }
        }

        /// <summary>
        /// Assign a parcel to a drone.
        /// </summary>
        /// <param name="DroneID">The id<see cref="int"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AttacheDrone(int DroneID)
        {
            lock (dal)
            {
                if (!FindDrone(DroneID).HaveParcel)
                {
                    if (!ParcelsNotAssociated().Any())
                        throw new ThereIsNoParcelToAttachdException("There is no parcel to attached");
                    var parcel = ParcelsNotAssociated().OrderByDescending(i => i.Priority).OrderByDescending(i => i.Weight).First();
                    dal.AttacheDrone(parcel.ID);
                }
                else
                    throw new DroneIsBusyException($"the drone {DroneID} in busy right new");
            }
        }
        public void AttacheDroneParcelID(int ParcelID)
        {
            lock (dal)
            {
                dal.AttacheDrone(ParcelID);
            }
        }

        /// <summary>
        /// Collection of a parcel by drone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
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
        public void PickUpParcelParcelID(int id)
        {
            try
            {
                lock (dal)
                {
                    dal.PickupParcel(id);
                }
            }
            catch (Exception) { throw new Exception(); }
        }

        /// <summary>
        /// Delivery of a parcel by drone.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
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
        public void ParceldeliveryParcelID(int id)
        {
            lock (dal)
            {
                dal.DeliverdParcel(id);
            }
        }
        /// <summary>
        /// station search.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>found station.</returns>
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
        /// drone search.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>found drone.</returns>
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

                    parcelTransactiningTemp.ID = p.ID;
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
        /// parcel search.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>found parcel.</returns>
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
                    ID = p.ID,
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
        /// customer search.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>found customer.</returns>
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
                    Email = c.Email,
                    Password = c.Password,
                };
                List<ParcelInCustomer> TempFromCustomer = new();
                ParcelInCustomer item = new();
                foreach (var item1 in p)
                {
                    if (item1.SenderId == id)
                    {
                        item.ID = item1.ID;
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
                        item2.ID = item3.ID;
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

        /// <summary>
        /// return all the stations.
        /// </summary>
        /// <returns>the stations.</returns>
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
        /// return all the drones.
        /// </summary>
        /// <returns>the drones.</returns>
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
        /// return all the parcels.
        /// </summary>
        /// <returns>the parcels.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> Parcels()
        {
            lock (dal)
            {
                return from p in dal.Parcellist()
                       select new ParcelToList()
                       {
                           ID = p.ID,
                           Priority = (Priority)p.Priority,
                           status = (StatusParcel)p.Status,
                           SenderName = Findparcel(p.ID).sender.CustomerName,
                           TargetName = Findparcel(p.ID).target.CustomerName,
                       };
            }
        }

        /// <summary>
        /// return all the customers.
        /// </summary>
        /// <returns>the customers.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerToList> Customers()
        {
            lock (dal)
            {
                return from c in dal.Customerlist()
                       select new CustomerToList()
                       {
                           Email = c.Email,
                           Password = c.Password,
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
        /// return all the parcels are not associated.
        /// </summary>
        /// <returns>the parcels are not associated.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> ParcelsNotAssociated()
        {
            lock (dal)
            {
                return from p in dal.ParcelNotAssociatedList()
                       select new ParcelToList()
                       {
                           ID = p.ID,
                           Priority = (Priority)p.Priority,
                           status = (StatusParcel)p.Status,
                           SenderName = Findparcel(p.ID).sender.CustomerName,
                           TargetName = Findparcel(p.ID).target.CustomerName,
                       };
            }
        }

        /// <summary>
        /// return all the free chargeslots.
        /// </summary>
        /// <returns>the free chargeslots.</returns>
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

        /// <summary>
        /// The DeleteParcel.
        /// </summary>
        /// <param name="parcel">The parcel<see cref="Parcel"/>.</param>
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

        /// <summary>
        /// The DeleteStation.
        /// </summary>
        /// <param name="station">The station<see cref="Station"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station station)
        {
            lock (dal)
                dal.DeleteStation(dal.FindStation(station.ID));
        }

        /// <summary>
        /// The DeleteCustomer.
        /// </summary>
        /// <param name="customer">The customer<see cref="Customer"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer customer)
        {
            lock (dal)
                dal.DeleteCustomer(dal.FindCustomers(customer.ID));
        }

        /// <summary>
        /// The DeleteDrone.
        /// </summary>
        /// <param name="drone">The drone<see cref="Drone"/>.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone drone)
        {
            lock (dal)
            {
                if (drone.Status == Status.MAINTENANCE && drone.Status == Status.FREE)
                    dal.DeleteDrone(dal.FindDrone((int)drone.ID));
                else
                    throw new CantDeleteException($"you can't delete this drone: {drone.ID}");
            }
        }

        /// <summary>
        /// The Uploader.
        /// </summary>
        /// <param name="droneId">The droneId<see cref="int"/>.</param>
        /// <param name="display">The display<see cref="Action"/>.</param>
        /// <param name="checker">The checker<see cref="bool"/>.</param>
        public void Uploader(int droneId, Action display, bool checker)
        {
            BL bl = new BL();
            Simulator simulator = new Simulator(bl, droneId, display, checker);//constractor
        }
    }
}