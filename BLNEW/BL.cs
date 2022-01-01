using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBL;
using IDAL;
//using IDAL.DO;
using BL.BO;
using BLExceptions;
using DateTime = System.DateTime;


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
        public void AddDrone(int id, string name, BO.WEIGHT weight,int IDStarting)
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
        public void AddParcel(int SenderId, int TargetId, BO.WEIGHT weight, BO.PRIORITY Priority)
        {
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
                    DroneId = null,

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
            DataSource.drones.Add(d);
        }
#nullable enable
        public void UpdateStation(int id,string ? name ,int ? TotalChargeslots)
        {
            IDAL.DO.Station s = dal.FindStation(id);
            s.StationName = name;
            s.ChargeSlots = (int)TotalChargeslots;
            foreach (var item in DataSource.stations)
            {
                if (item.ID == id)
                {
                    DataSource.stations.Remove(item);
                }
            }
            DataSource.stations.Add(s);
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
            DataSource.customers.Add(c);
        }
#nullable disable
        public void DroneToCharge(int id)
        {
            IDAL.DO.Drone d = dal.FindDrone(id);
            if(d.Status != 0||d.Buttery < 20)
            {//Buttery???????????????????????????/
                //trow exception
            }
            else
            {

            }
        }
        public void DroneOutCharge(int id,TimeSpan time)
        {

        }
        public void AttacheDrone(int id)
        {

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
            int count = 0;
            List<DroneCharging> droneChargingTemp = new();
            foreach (var item in DataSource.droneCharges)
            {
                if (item.StationId==id)
                {
                    droneChargingTemp[count].ID = (int)item.DroneId;
                    droneChargingTemp[count].Buttery= (dal.FindDrone(id)).Buttery;
                    count++;
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
            if (d.Status == IDAL.DO.STATUS.DELIVERING)
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
                parcelTransactiningTemp.distance = Math.Sqrt((Math.Pow(s.Lattitude - t.Lattitude, 2) + Math.Pow(s.Longitude - t.Longitude, 2)));
            }
            Drone newStation = new Drone()
            {
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
                    fromCustomer.target.CustomerName = findcustomer(item.TargetId).CustomerName;//bl??dl??
                    newCustomer.fromCustomer.Add(fromCustomer);
                }

                if ((item.TargetId == id)&& (item.Deliverd < DateTime.Now))
                {
                    ToCustomer.ID = (int)item.ID;
                    ToCustomer.weight = (WEIGHT)item.Weight;
                    ToCustomer.priority = (PRIORITY)item.Priority;
                    ToCustomer.status = (STATUS)3;
                    ToCustomer.sender.ID = item.TargetId;
                    ToCustomer.sender.CustomerName = findcustomer(item.TargetId).CustomerName;//bl??dl??
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


    }


}