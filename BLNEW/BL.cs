﻿using BLExceptions;
using DAL;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DateTime = System.DateTime;
namespace BL
{
    public sealed class BL : BlApi.IBL//
    {
        readonly IDal dal = DalFactory.GetDal("DalObject");
        /// <summary>
        /// constractor
        /// </summary> 

        private BL()
        {
            DO.Drone tempDrone = new();
            IEnumerable<DO.Parcel> p = dal.Parcellist();
            Random random = new();
            foreach (DO.Parcel i in p)
            {
  ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////לא סגור 
                 if (i.DroneId != 0)
                 {
                    tempDrone = dal.FindDrone((int)i.DroneId); //tempDrone = dal.FindDrone((int)i.DroneId);//השמה של הנתונים הישנים

                    if ((i.Scheduled != null) && (i.Deliverd == null))
                     {
                         tempDrone.Status = (DO.Status)2;
                         if ((i.PickedUp == null) && (i.Scheduled != null))//שויכה אבל לא נאספה
                         {//shortest station
                             Location sta = new()
                             {
                                 Lattitude = 0,
                                 Longitude = 0,
                             };
                             double d = 0;
                             foreach (DO.Station item in dal.Freechargeslotslist())
                             {
                                 if (Distans(FindStation((int)item.ID).Position, Findcustomer(i.SenderId).Position) > d)
                                 {
                                     d = Distans(FindStation((int)item.ID).Position, Findcustomer(i.SenderId).Position);
                                     sta = FindDrone((int)i.DroneId).Position;
                                 }
                             }

                             tempDrone.Lattitude = sta.Lattitude;
                             tempDrone.Longitude = sta.Longitude;
                         }
                         //מפה כל מה שאני אמרתי לך לטפל
                         if ((i.Deliverd == null) && (i.PickedUp != null))
                         {
                             //FindDrone((int)i.DroneId).Position = FindStation(i.SenderId).Position;//בעיה - צריך להשוות את הרחפן *לשולח
                             tempDrone.Lattitude = dal.FindStation(i.SenderId).Lattitude;
                             tempDrone.Longitude = dal.FindStation(i.SenderId).Longitude;
                         }
                         tempDrone.Battery = random.Next(MinPower(FindDrone((int)i.DroneId)), 100);//need to check minpower
                     }
                    if (FindDrone((int)i.DroneId).Status != (Status)1)//אם הרחפן לא מבצע משלוח
                    {
                        tempDrone.Status = (DO.Status)random.Next(3, 5);
                        if (FindDrone((int)i.DroneId).Status == Status.BELONG)
                        {
                            tempDrone.Status = DO.Status.CREAT;
                        }
                    }
                    if (FindDrone((int)i.DroneId).Status == Status.MAINTENANCE)//
                    {
                        //List<Station> s = new();
                        tempDrone.Lattitude = dal.FindStation(FreeChargeslots().ToList()[random.Next(0, FreeChargeslots().Count() - 1)].ID).Lattitude;
                        tempDrone.Longitude = dal.FindStation(FreeChargeslots().ToList()[random.Next(0, FreeChargeslots().Count() - 1)].ID).Longitude;
                        tempDrone.Battery = random.Next(0, 21);
                    }
                     if (FindDrone((int)i.DroneId).Status == Status.CREAT)
                    {
                        List<Parcel> pa = new();
                        foreach (var item in Parcels())
                        {
                            pa = pa.FindAll(delegate (Parcel p) { return (p.Deliverd != null); });//לקוח שקיבל חבילה
                        }
                        if (pa.Count == 0)
                        {
                            tempDrone.Lattitude = dal.FindCustomers(Customers().ToList()[random.Next(0, Customers().Count() - 1)].ID).Lattitude;
                            tempDrone.Longitude = dal.FindCustomers(Customers().ToList()[random.Next(0, Customers().Count() - 1)].ID).Longitude;
                        }
                        else
                        {
                            tempDrone.Lattitude = dal.FindCustomers(pa[random.Next(0, pa.Count - 1)].target.ID).Lattitude;
                            tempDrone.Longitude = dal.FindCustomers(pa[random.Next(0, pa.Count - 1)].target.ID).Longitude;
                        }
                        //מספר רנדומלי מתוך כל הלקוחות שקיבלו חבילה בו אני מחפש את האיידיי של המקבל שם בחיפוש לקוח ולוקח ממנו את המיקום
                        tempDrone.Battery = random.Next(MinPower(FindDrone((int)i.DroneId)), 100);
                    }
                    dal.UpdateDrone(tempDrone);
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////לא סגור 
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// סגור 
        static BL instance = null;
        public static BL GetInstance()
        {
            if (instance == null)
                instance = new BL();
            return instance;
        }    
        int MinPower(Drone drone)
        {
            double a=0;
            int c = 0;
            int? StationID;
            foreach (var item in dal.Stationlist())
            {
                Location location = new Location()
                {
                    Lattitude = item.Lattitude,
                    Longitude = item.Longitude,
                };

                if ((a < Distans(location, drone.Position))&&c!=0)
                {
                    a = Distans(location, drone.Position);
                    StationID = item.ID;
                } 
                if (c == 0) 
                {
                    a = Distans(location, drone.Position);
                    c++;
                }
            }
            double i = dal.Power()[((int)drone.Weight + 1) % 4];
            i *= a;
            i = Math.Ceiling(i);
            return (int)i;
        }
        /// <summary>
        /// Distans
        /// </summary>
        /// <returns>Distans between a - b</returns>
        static private double Distans(Location a, Location b)
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
            try
            {
                DO.Station tempStation = new()
                {
                    ID = station.ID,
                    StationName = station.StationName,
                    Longitude = station.Position.Longitude,
                    Lattitude = station.Position.Lattitude,
                    ChargeSlots = station.ChargeSlots,
                };
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
        readonly Random ran = new Random();
        public void AddDrone(Drone drone, int IDStarting)
        {
            try
            {
                DO.Drone tempDrone = new()
                {
                    ID = drone.ID,
                    Model = drone.Model,
                    Weight = (DO.Weight)drone.Weight,
                    Battery = ran.Next(20,40),
                    haveParcel = false,
                };
                Location l = FindStation(IDStarting).Position;
                tempDrone.Lattitude = l.Lattitude;
                tempDrone.Longitude = l.Longitude;
                Station s = FindStation(IDStarting);
                DroneCharging temp = new() { ID = (int)drone.ID, Battery = drone.Battery };
                s.DroneChargingInStation.Add(temp);
                UpdateStation(s);
                dal.AddDrone(tempDrone);
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
                DO.Customer tempCustomer = new()
                {
                    ID = customer.ID,
                    CustomerName = customer.CustomerName,
                    Longitude = customer.Position.Longitude,
                    Lattitude = customer.Position.Lattitude,
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
            try
            {
                DO.Parcel tempParcel = new()
                {
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
        public void UpdateDrone(Drone drone) => dal.UpdateDrone(new DO.Drone
        {
            ID = drone.ID,
            Battery = drone.Battery,
            haveParcel = drone.HaveParcel,
            Lattitude = drone.Position.Lattitude,
            Longitude = drone.Position.Longitude,
            Model = drone.Model,
            Status = (DO.Status)drone.Status,
            Weight = (DO.Weight)drone.Weight,
        });

        /// <summary>
        /// Update Station details
        /// </summary>
#nullable enable
        public void UpdateStation(Station station) => dal.UpdateStation(new DO.Station
        {
            ID = station.ID,
            StationName = station.StationName,
            ChargeSlots = station.ChargeSlots,
            Lattitude = station.Position.Lattitude,
            Longitude = station.Position.Longitude,
            BusyChargeSlots = station.DroneChargingInStation.Count(),
        });

        /// <summary>
        /// Update Customer details
        /// </summary>
        public void UpdateCustomer(Customer customer) => dal.UpdateCustomer(new DO.Customer
        {
            ID = customer.ID,
            CustomerName = customer.CustomerName,
            Lattitude = customer.Position.Lattitude,
            Longitude = customer.Position.Longitude,
            Phone = customer.Phone,
        });
#nullable disable

        /// <summary>
        /// Inserting a drone from a charger
        /// </summary>
        public void DroneToCharge(int id)
        {
            DO.Drone d = dal.FindDrone(id);
            if ((d.Status == DO.Status.BELONG || d.Status == DO.Status.PICKUP || d.Status == DO.Status.MAINTENANCE) && d.Battery < 20 )
            {
                throw new DontHaveEnoughPowerException($"the drone {id} don't have enough power");
            }
            else
            {
                int StationID = Stations().OrderBy(i => Distans(FindStation(i.ID).Position, FindDrone(id).Position)).First().ID;
                //foreach (var item in Stations())
                //{
                //    if (Distans(FindStation(item.ID).Position, FindDrone(id).Position) > distans)
                //    {
                //        distans = Distans(FindStation(item.ID).Position, FindDrone(id).Position);
                //        sID = item.ID;
                //    }
                //}

                //מצב סוללה יעודכן בהתאם למרחק בין הרחפן לתחנה
                dal.DroneToCharge(id, StationID);
                //DataSource.drones.ForEach(delegate (DO.Drone drone)
                //{
                //    if (drone.ID == id)
                //    {
                //        drone.Lattitude = 2/*FindStation(sID).Position.Lattitude*/;
                //        drone.Longitude = 2/* FindStation(sID).Position.Longitude*/;
                //        drone.Status = DO.Status.MAINTENANCE;
                //    }
                //});
                //FindStation(sID).FreeChargeSlots--;
                //dal.AddDroneCharge(sID, id);
                //DO.DroneCharge droneCharge = DataSource.droneCharges.Find(delegate (DO.DroneCharge drone) { return (int)drone.DroneId == id; });
                //DroneCharging droneCharging1 = new()
                //{
                //    ID = (int)droneCharge.DroneId,
                //    Battery = (dal.FindDrone((int)droneCharge.DroneId)).Battery,
                //};
                //FindStation(sID).DroneChargingInStation.Add(droneCharging1);
            }
        }
        /// <summary>
        /// Removing a drone from a charger
        /// </summary>
        public void DroneOutCharge(int id, double time)
        {

            if (FindDrone(id).Status == Status.MAINTENANCE)
            {
                Drone drone = FindDrone(id);
                drone.Status = Status.CREAT;
                drone.Battery = dal.Power()[4] * (time/60);
                var temp = dal.DroneChargelist().Where(i => i.DroneId == id).ToList();
                Station station = FindStation(temp[0].StationId);
                int index  = station.DroneChargingInStation.FindIndex(i => i.ID == id);
                station.DroneChargingInStation.RemoveAt(index);
                UpdateDrone(drone);
                dal.DroneOutCharge(id);
            }
            else
            {
                throw new DroneDontInChargingException($"The Drone {id} Doesn't In Charging");
            }
        }
        
        /// <summary>
        /// Assign a parcel to a drone
        /// </summary>
        public void AttacheDrone(int id)
        {
            if (!FindDrone(id).HaveParcel)
            {
                if (ParcelsNotAssociated().Count() == 0)
                    throw new ThereIsNoParcelToAttachdException("There is no parcel to attachd");
                var parcel = ParcelsNotAssociated().OrderBy(i => i.Priority).First();
                dal.AttacheDrone(parcel.ID);

                //List<ParcelToList> temp = ParcelsNotAssociated().ToList();
                //List<ParcelToList> temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.SOS; });
                //if (temp1.Count == 0)
                //{
                //    temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.FAST; });
                //    if (temp1.Count == 0)
                //    {
                //        temp1 = temp.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.REGULAR; });
                //        if (temp1.Count == 0)
                //        {
                //            throw new ThereIsNoParcelToAttach("there are no parcel to attach");
                //        }
                //    }
                //}
                //temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.SOS; });
                //if (temp1.Count == 0)
                //{
                //    temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.FAST; });
                //    if (temp1.Count == 0)
                //    {
                //        temp1 = temp1.FindAll(delegate (ParcelToList p) { return p.Priority == Priority.REGULAR; });
                //        if (temp1.Count == 0)
                //        {
                //            throw new ThereIsNoParcelToAttach("there are no parcel to attach");
                //        }
                //    }
                //}
                //Location location = new()
                //{ Lattitude = 0, Longitude = 0, };
                //int saveID = 0;//בטוח ידרס
                //temp1.ForEach(delegate (ParcelToList p)
                //{
                //    if (Distans(FindDrone(id).Position, Findcustomer(Findparcel(p.ID).sender.ID).Position) > Distans(FindDrone(id).Position, location))
                //    {
                //        location.Lattitude = Findcustomer(Findparcel(p.ID).sender.ID).Position.Lattitude;
                //        location.Longitude = Findcustomer(Findparcel(p.ID).sender.ID).Position.Longitude;
                //        saveID = p.ID;
                //    }
                //});
                ////foreach (var item in temp1)
                ////{
                ////    if (Distans(FindDrone(id).Position, Findcustomer(Findparcel(item.ID).sender.ID).location) > Distans(FindDrone(id).Position, location))
                ////    {
                ////        location.Lattitude = Findcustomer(Findparcel(item.ID).sender.ID).location.Lattitude;
                ////        location.Longitude = Findcustomer(Findparcel(item.ID).sender.ID).location.Longitude;
                ////        saveID = item.ID;
                ////    }
                ////}
                //Findparcel(saveID).Drone.ID = id;
                //Findparcel(saveID).Drone.Battery = FindDrone(id).Battery;
                //Findparcel(saveID).Drone.Position = FindDrone(id).Position;
                //Findparcel(saveID).Scheduled = DateTime.Now;
                //FindDrone(id).Status = Status.BELONG;
            }
            else
                throw new DroneIsBusyException($"the drone {id} in busy right new");
        }

        /// <summary>
        /// Collection of a parcel by drone
        /// </summary>
        public void PickUpParcel(int id)
        {
            try
            {
                int t = (int)FindDrone(id).Parcel.ID;
                if (Findparcel(t).PickedUp != null)
                    throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have picked up");
                else
                    dal.PickupParcel(t);
            }
            catch (Exception) { throw new ParcelPastErroeException($"there are no parcel to pickup"); }
            //if ( == Status.BELONG)
            //{
            //    switch (FindDrone(id).Weight)
            //    {
            //        case Weight.LIGHT:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Position) * dal.Power()[(int)Weight.LIGHT]);
            //            break;
            //        case Weight.MEDIUM:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Position) * dal.Power()[(int)Weight.MEDIUM]);
            //            break;
            //        case Weight.HEAVY:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Position) * dal.Power()[(int)Weight.HEAVY]);
            //            break;
            //        case Weight.FREE:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Position) * dal.Power()[(int)Weight.FREE]);
            //            break;
            //    }
            //    FindDrone(id).Position = FindDrone(id).Parcel.LocationOfSender;
            //    Findparcel((int)FindDrone(id).Parcel.ID).PickedUp = DateTime.Now;
            //}
            //else
            //{
            //    if (FindDrone(id).HaveParcel)
            //        throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have picked up");
            //    else
            //        throw new ParcelPastErroeException($"there are no parcel to pickup");
            //}
        }
        
        /// <summary>
        /// Delivery of a parcel by drone
        /// </summary>
        public void Parceldelivery(int id)
        {
            try
            {
                int t = (int)FindDrone(id).Parcel.ID;
                if (Findparcel(t).Deliverd != null)
                    throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have deliverd up");
                else
                    dal.DeliverdParcel(t);
            }
            catch (Exception) { throw new ParcelPastErroeException($"there are no parcel to deliverd"); }
            //if (FindDrone(id).Status == Status.PICKUP)
            //{
            //    switch (FindDrone(id).Weight)
            //    {
            //        case Weight.LIGHT:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Parcel.LocationOftarget) * dal.Power()[(int)Weight.LIGHT]);
            //            break;
            //        case Weight.MEDIUM:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Parcel.LocationOftarget) * dal.Power()[(int)Weight.MEDIUM]);
            //            break;
            //        case Weight.HEAVY:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Parcel.LocationOftarget) * dal.Power()[(int)Weight.HEAVY]);
            //            break;
            //        case Weight.FREE:
            //            FindDrone(id).Battery = FindDrone(id).Battery - (Distans(FindDrone(id).Parcel.LocationOfSender, FindDrone(id).Parcel.LocationOftarget) * dal.Power()[(int)Weight.FREE]);
            //            break;
            //    }
            //    FindDrone(id).Position = FindDrone(id).Parcel.LocationOftarget;
            //    FindDrone(id).Status = Status.CREAT;
            //    Findparcel((int)FindDrone(id).Parcel.ID).Deliverd = DateTime.Now;
            //}
            //else
            //{
            //    if (FindDrone(id).Parcel == null)
            //        throw new ParcelPastErroeException($"the {FindDrone(id).Parcel.ID} already have delivered");
            //}
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
            DO.Station s = dal.FindStation(id);
            List<DroneCharging> droneChargingTemp = new();
            var droneChargingfiltered = dal.DroneChargelist().Where(i => i.StationId == id).ToList();
            droneChargingfiltered.ForEach(i => droneChargingTemp.Add(new() { ID = (int)i.DroneId, Battery = FindDrone((int)i.DroneId).Battery }));
            //foreach (var item in DataSource.droneCharges)
            //{
            //    if (item.StationId == id)
            //    {
            //        DroneCharging droneCharging1 = new()
            //        {
            //            ID = (int)item.DroneId,
            //            Battery = (dal.FindDrone((int)item.DroneId)).Battery,
            //        };
            //        droneChargingTemp.Add(droneCharging1);
            //    }
            //}
            Location temp = new()
            {
                Lattitude = s.Lattitude,
                Longitude = s.Longitude,
            };
            Station newStation = new()
            {
                ID = (int)s.ID,
                StationName = s.StationName,
                Position = temp,
                ChargeSlots = s.ChargeSlots,
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
            DO.Drone d = dal.FindDrone(id);
            ParcelTransactioning parcelTransactiningTemp = new();
            parcelTransactiningTemp.ID = null;
            Drone newStation = new();
            newStation.HaveParcel = d.haveParcel;
            newStation.ID = d.ID;
            newStation.Model = d.Model;
            newStation.Weight = (Weight)d.Weight;
            newStation.Status = (Status)d.Status;
            newStation.Battery = d.Battery;
            Location locationDrone = new()
            {
                Lattitude = d.Lattitude,
                Longitude = d.Longitude,
            };
            newStation.Position = locationDrone;

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
                parcelTransactiningTemp.distance = Distans(locationSend, locationTarget);


            }
            newStation.Parcel = parcelTransactiningTemp;
            return newStation;
        }
       
        /// <summary>
        /// parcel search
        /// </summary>
        /// <returns>found parcel</returns>
        public Parcel Findparcel(int id)//סיימתי
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
            if (p.DroneId!=0)
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
        
        /// <summary>
        /// customer search
        /// </summary>
        /// <returns>found customer</returns>
        public Customer Findcustomer(int id)
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
            //List<StationToList> temp1 = new();
            //List<Station> stations = new();
            //foreach (var item in dal.Stationlist())
            //{
            //    stations.Add(FindStation((int)item.ID));
            //}
            //for (int i = 0; i < stations.Count; i++)
            //{
            //    StationToList temp = new();
            //    temp.ID = stations[i].ID;
            //    temp.StationName = stations[i].StationName;
            //    temp.FreeChargeSlots = stations[i].ChargeSlots - stations[i].DroneChargingInStation.Count();
            //    temp.UsedChargeSlots = stations[i].DroneChargingInStation.Count();
            //    temp1.Add(temp);
            //}
            return from s in dal.Stationlist()
                    select new StationToList()
                    {
                        ID = (int)s.ID,
                        StationName = s.StationName,
                        FreeChargeSlots = s.ChargeSlots - s.BusyChargeSlots,
                        UsedChargeSlots = s.BusyChargeSlots
                    };

        }
        
        /// <summary>
        /// reture all the drones
        /// </summary>
        /// <returns>the drones</returns>
        public IEnumerable<DroneToList> Drones()
        {
            //List<Drone> drones = new(); 
            //List<DroneToList> droneToList = new ();
            //int i = 0;
            //foreach (var item in dal.Dronelist())
            //{
            //    drones.Add(FindDrone((int)item.ID));
            //    DroneToList droneToList1 = new();
            //    droneToList1.Id = (int)drones[i].ID;
            //    try
            //    {
            //        droneToList1.IdParcel = drones[i].Parcel.ID;
            //    }
            //    catch (Exception)
            //    {
            //        droneToList1.IdParcel = null;
            //    }
            //    droneToList1.Model = drones[i].Model;
            //    droneToList1.Status = drones[i].Status;
            //    droneToList1.Weight = drones[i].Weight;
            //    droneToList1.Battery = drones[i].Battery;
            //    droneToList1.Position = drones[i].Position;
            //    droneToList.Add(droneToList1);
            //    i++;
            //}
            //return droneToList;
            return from d in dal.Dronelist()
                   select new DroneToList()
                   {
                       Id = (int)d.ID,
                       Battery = d.Battery,
                       IdParcel = FindDrone((int)d.ID).Parcel.ID,
                       Model = d.Model,
                       Position = FindDrone((int)d.ID).Position,
                       Status = (Status)d.Status,
                       Weight = (Weight)d.Weight,
                   };
        }
        
        /// <summary>
        /// reture all the parcels
        /// </summary>
        /// <returns>the parcels</returns>
        public IEnumerable<ParcelToList> Parcels()
        {
            //List<Parcel> parcels = new();
            //List<ParcelToList> temp = new();
            //int i = 0;
            //foreach (var item in dal.Parcellist())
            //{
            //    parcels.Add(Findparcel((int)item.ID));
            //    ParcelToList parceltolist = new();
            //    parceltolist.ID = parcels[i].ID;
            //    parceltolist.Priority = parcels[i].Priority;
            //    parceltolist.SenderName = parcels[i].sender.CustomerName;
            //    if (parcels[i].Requested < DateTime.Now && parcels[i].Requested != null)
            //    {
            //        parceltolist.status = (Status)0;
            //    }
            //    if (parcels[i].Scheduled < DateTime.Now && parcels[i].Scheduled != null)
            //    {
            //        parceltolist.status = (Status)1;
            //    }
            //    if (parcels[i].PickedUp < DateTime.Now && parcels[i].PickedUp != null)
            //    {
            //        parceltolist.status = (Status)2;
            //    }
            //    if (parcels[i].Deliverd < DateTime.Now && parcels[i].Deliverd != null)
            //    {
            //        parceltolist.status = (Status)3;
            //    }
            //    if (parcels[i].Deliverd != null && parcels[i].Scheduled == null)
            //    {
            //        parceltolist.status = (Status)0;
            //    }
            //    parceltolist.TargetName = parcels[i].target.CustomerName;
            //    parceltolist.Weight = parcels[i].Weight;
            //    temp.Add(parceltolist);
            //    i++;
            //}
            //return temp;
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

        /// <summary>
        /// reture all the customers
        /// </summary>
        /// <returns>the customers</returns>
        public IEnumerable<CustomerToList> Customers()
        {
            //List<CustomerToList> temp = new();
            //List<Customer> customer = new();
            //int counter1 = 0, counter2 = 0, i = 0;
            //foreach (var item in dal.Customerlist())
            //{
            //    customer.Add(Findcustomer((int)item.ID));          
            //    CustomerToList customerToList=new();
            //    customerToList.ID = customer[i].ID;
            //    customerToList.CustomerName = customer[i].CustomerName;
            //    counter1 = 0;
            //    counter2 = 0;
            //    foreach (var item1 in customer[i].toCustomer)
            //    {
            //        if (item1.Status != Status.PROVID)
            //            counter1++;
            //        else
            //            counter2++;
            //    }
            //    customerToList.NumFoParcelOnWay = counter1;
            //    counter1 = 0;
            //    customerToList.NumFoParcelReceived = counter2;
            //    counter2 = 0;
            //    foreach (var item2 in customer[i].fromCustomer)
            //    {
            //        if (item2.Status != Status.PROVID)
            //            counter1++;
            //        else
            //            counter2++;
            //    }
            //    customerToList.NumFoParcelSent = counter1;
            //    customerToList.NumFoParcelSentAndDelivered = counter2;
            //    customerToList.Phone = customer[i].Phone;
            //    temp.Add(customerToList);
            //    i++;
            //}
            //return temp;
            return from c in dal.Customerlist()
                   select new CustomerToList()
                   {
                       ID = (int)c.ID,
                       CustomerName = c.CustomerName,
                       Phone = c.Phone,
                       NumFoParcelSent = Findcustomer((int)c.ID).fromCustomer.Count(),
                       NumFoParcelOnWay = Findcustomer((int)c.ID).toCustomer.Count(),
                       NumFoParcelReceived = Findcustomer((int)c.ID).toCustomer.Count(i=>i.Status == StatusParcel.DELIVERD),
                       NumFoParcelSentAndDelivered = Findcustomer((int)c.ID).fromCustomer.Count(i => i.Status == StatusParcel.DELIVERD),
                   };

        }
        /// <summary>
        /// reture all the parcels are not associated
        /// </summary>
        /// <returns>the parcels are not associated</returns>
        public IEnumerable<ParcelToList> ParcelsNotAssociated()
        {
            //List<Parcel> parcels = new();
            //List<ParcelToList> temp = new();
            //int i = 0;
            //if (dal.ParcelNotAssociatedList().Count() > 0)
            //{
            //    foreach (var item in dal.ParcelNotAssociatedList())
            //    {
            //        parcels.Add(Findparcel((int)item.ID));
            //        ParcelToList parcelToList = new();
            //        parcelToList.ID = parcels[i].ID;
            //        parcelToList.Priority = parcels[i].Priority;
            //        parcelToList.SenderName = parcels[i].sender.CustomerName;
            //        if (parcels[i].Requested < DateTime.Now && parcels[i].Requested != null)
            //        {
            //            parcelToList.status = (Status)0;
            //        }
            //        if (parcels[i].Scheduled < DateTime.Now && parcels[i].Scheduled != null && parcels[i].Drone.ID==0)
            //        {
            //            parcelToList.status = (Status)1;
            //        }
            //        if (parcels[i].PickedUp < DateTime.Now && parcels[i].PickedUp != null)
            //        {
            //            parcelToList.status = (Status)2;
            //        }
            //        if (parcels[i].Deliverd < DateTime.Now && parcels[i].Deliverd != null)
            //        {
            //            parcelToList.status = (Status)3;
            //        }
            //        if (parcels[i].Deliverd != null && parcels[i].Scheduled == null)
            //        {
            //            parcelToList.status = (Status)0;
            //        }
            //        parcelToList.TargetName = parcels[i].target.CustomerName;
            //        parcelToList.Weight = parcels[i].Weight;
            //        temp.Add(parcelToList);
            //        i++;
            //    }
            //}
            //return temp;
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
        
        /// <summary>
        /// reture all the free chargeslots
        /// </summary>
        /// <returns>the free chargeslots</returns>
        public IEnumerable<StationToList> FreeChargeslots()
        {
            //List<StationToList> temp1 = new();
            //List<Station> stations = new();
            //int i = 0;
            //foreach (var item in dal.Stationlist())
            //{
            //    if (item.ChargeSlots - item.BusyChargeSlots >= 1)
            //    {
            //        stations.Add(FindStation((int)item.ID));
            //        StationToList temp = new();
            //        temp.ID = stations[i].ID;
            //        temp.StationName = stations[i].StationName;
            //        temp.FreeChargeSlots = stations[i].ChargeSlots - stations[i].DroneChargingInStation;
            //        temp.UsedChargeSlots = 5 - stations[i].ChargeSlots;//חייבים לבדוק כל הזמן שזה לא שלילי אם זה שלילי חייבים לברר מה ההבעיה
            //        temp1.Add(temp);
            //        i++;
            //    }
            //}
            //return temp1;
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
}