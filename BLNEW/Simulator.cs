using BLExceptions;
using BO;
using System;
using System.Linq;
using System.Threading;
//ntc the delay

namespace BL
{
    public class Simulator
    {
        private readonly double speed = 60;//---km/h
        private readonly int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(int droneId, Action display, Func<bool> checker, BL bl)//constractor
        {
            bool b = true;
            while (b)
            {
                b = checker();
                Thread.Sleep(2500);
                Drone drone;
                drone = bl.FindDrone(droneId);
                switch (drone.Status)
                {
                    case Status.FREE:
                        lock (bl)
                        {
                            if (!bl.ParcelsNotAssociated().Any())
                            {
                                bl.DroneToCharge(droneId, true);
                                b=false;
                            }
                            try
                            {
                                bl.AttacheDrone(droneId);
                            }
                            catch (DontHaveEnoughPowerException ex)
                            {
                                throw new DontHaveEnoughPowerException(ex.Message);
                            }
                            catch (ThereIsNoParcelToAttachException)
                            {
                                Thread.Sleep(DELAY);
                            }
                        }
                        break;
                    case Status.BELONG:
                        try
                        {
                            lock (bl)
                            {
                                //double a = PowerConsumption(Distance(d.Position, d.Parcel.LocationOfSender), d.Parcel.weight);
                                //if (a < d.Battery)
                                //{
                                //    d.Parcel.distance = Distance(d.Position, d.Parcel.LocationOfSender);
                                //    bl.PickUpParcel(droneId);
                                //    //i = 0;
                                //    Thread.Sleep((int)(Distance(d.Position, d.Parcel.LocationOfSender) / speed));
                                //}
                                //else
                                //{
                                //    Thread.Sleep(DELAY);
                                //    d.Status = Status.FREE;
                                //    bl.UpdateDrone(d);
                                //    bl.DroneToCharge(droneId);
                                //    DateTime time = DateTime.Now;
                                //    do
                                //    {
                                //        System.Threading.Thread.Sleep(500);
                                //        d.Status = Status.MAINTENANCE;
                                //        bl.UpdateDrone(d);
                                //        bl.DroneOutCharge(droneId, DateTime.Now);
                                //        d = bl.FindDrone(droneId);
                                //        d.Status = Status.BELONG;
                                //        bl.UpdateDrone(d);
                                //    } while (d.Battery < a);
                                //    bl.PickUpParcel(droneId);
                                //    //bl.AddBattery(droneId, -bl.GetElectricityPerKM((DELAY / 1000) * speed, d.Parcel.weight));
                                //    //TODO bonus location
                                //}
                                try
                                {
                                    drone.Status = BO.Status.MAINTENANCE;
                                    bl.UpdateDrone(drone);
                                    bl.DroneOutCharge((int)drone.ID, DateTime.Now);
                                    drone.Status = BO.Status.BELONG;
                                    bl.UpdateDrone(drone);
                                }
                                catch (DroneDontInChargingException ex)
                                {
                                    throw new DroneDontInChargingException(ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                                bl.PickUpParcel(droneId);
                            }
                        }
                        catch (ParcelPastErroeException)
                        {
                            Thread.Sleep(DELAY);
                        }
                        break;
                    case Status.PICKUP:
                        {
                            lock (bl)
                            {
                                //try
                                //{
                                //    double a = PowerConsumption(Distance(drone.Position, drone.Parcel.LocationOftarget), drone.Parcel.weight);
                                //    if (a < drone.Battery)
                                //    {
                                //        drone.Parcel.distance = Distance(drone.Position, drone.Parcel.LocationOftarget);
                                //        bl.Parceldelivery(droneId);
                                //        //i = 0;
                                //        Thread.Sleep((int)(Distance(drone.Position, drone.Parcel.LocationOfSender) / speed));
                                //        drone.Parcel.distance = 0;
                                //    }
                                //    else
                                //    {
                                //        Thread.Sleep(DELAY);
                                //        drone.Status = Status.FREE;
                                //        bl.UpdateDrone(drone);
                                //        bl.DroneToCharge(droneId);
                                //        DateTime time = DateTime.Now;
                                //        do
                                //        {
                                //            System.Threading.Thread.Sleep(500);
                                //            drone.Status = Status.MAINTENANCE;
                                //            bl.UpdateDrone(drone);
                                //            bl.DroneOutCharge(droneId, DateTime.Now);
                                //            drone = bl.FindDrone(droneId);
                                //            drone.Status = Status.PICKUP;
                                //            bl.UpdateDrone(drone);
                                //        } while (drone.Battery < a);
                                //        bl.Parceldelivery(droneId);
                                //    }
                                //}
                                try
                                {
                                    bl.Parceldelivery(droneId);
                                }

                                catch (ParcelPastErroeException)
                                {
                                    Thread.Sleep(DELAY);
                                }

                                catch (Exception ex)
                                {
                                    new Exception(ex.Message.ToString());
                                }
                            }
                        }
                        break;
                    case Status.MAINTENANCE:
                        lock (bl)
                        {
                            //    Thread.Sleep(DELAY);
                            //    if (d.Battery == 100)
                            //    {
                            //        bl.DroneOutCharge(droneId, 60, false);
                            //        //i = 0;
                            //        break;
                            //    }
                            //bl.AddBattery(droneId, (DELAY / 1000) * bl.ChargePerSecond);
                            //DateTime time = DateTime.Now;
                            //while (d.Battery < 100)
                            //
                            //    Thread.Sleep(500);
                            //    bl.DroneOutCharge((int)d.ID, (DateTime.Now - time).TotalMinutes);
                            //}
                            DateTime dateTime = DateTime.Now;
                            do
                            {
                                Thread.Sleep(500);
                                drone.Status = Status.MAINTENANCE;
                                bl.UpdateDrone(drone);
                                bl.DroneOutCharge((int)drone.ID, DateTime.Now);
                                drone = bl.FindDrone((int)drone.ID);
                                drone.Status = Status.FREE;
                                bl.UpdateDrone(drone);
                            } while (drone.Battery < 100);
                        }
                        break;
                }
            }
        }
    }
}