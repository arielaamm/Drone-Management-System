using BLExceptions;
using BO;
using System;
using System.Linq;
using System.Threading;
using static BL.BL;
//ntc the delay

namespace BL
{
    public class Simulator
    {
        private readonly double speed = 60;//---km/h
        private readonly int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(int droneId, Action display, Func<bool> checker, BL bl)//constractor
        {
            while (checker() && bl.ParcelsNotAssociated().Any())
            {
                Drone d;
                d = bl.FindDrone(droneId);
                switch (d.Status)
                {
                    case Status.FREE:
                        try
                        {
                            bl.AttacheDrone(droneId);
                        }
                        catch (DontHaveEnoughPowerException)
                        {
                            try
                            {
                                bl.DroneToCharge(droneId);
                            }
                            catch
                            {
                                //TODO
                                //  throw new BO.CantBeNegative(droneId);
                            }
                        }
                        catch (ThereIsNoParcelToAttachException)
                        {
                            Thread.Sleep(DELAY);
                        }
                        break;
                    case Status.BELONG:
                        try
                        {
                            double a = PowerConsumption(Distance(d.Position, d.Parcel.LocationOfSender), d.Parcel.weight);
                            if (a < d.Battery)
                            {
                                d.Parcel.distance = Distance(d.Position, d.Parcel.LocationOfSender);
                                bl.PickUpParcel((int)(d.Parcel.ID));
                                //i = 0;
                                Thread.Sleep((int)(Distance(d.Position, d.Parcel.LocationOfSender) / speed));
                            }
                            else
                            {
                                Thread.Sleep(DELAY);
                                d.Status = Status.FREE;
                                bl.UpdateDrone(d);
                                bl.DroneToCharge((int)d.ID);
                                DateTime time = DateTime.Now;
                                do
                                {
                                    System.Threading.Thread.Sleep(500);
                                    d.Status = Status.MAINTENANCE;
                                    bl.UpdateDrone(d);
                                    bl.DroneOutCharge((int)d.ID, (DateTime.Now - time).TotalMinutes);
                                    d = bl.FindDrone((int)d.ID);
                                    d.Status = Status.BELONG;
                                    bl.UpdateDrone(d);
                                } while (d.Battery <= 100);

                                //bl.AddBattery(droneId, -bl.GetElectricityPerKM((DELAY / 1000) * speed, d.Parcel.weight));
                                //TODO bonus location
                            }
                        }
                        catch (ParcelPastErroeException)
                        {
                            Thread.Sleep(DELAY);
                        }
                        break;
                    case Status.PICKUP:
                        {
                            try
                            {
                                double a = PowerConsumption(Distance(d.Position, d.Parcel.LocationOftarget), d.Parcel.weight);
                                if (a < d.Battery)
                                {
                                    d.Parcel.distance = Distance(d.Position, d.Parcel.LocationOftarget);
                                    bl.Parceldelivery((int)d.Parcel.ID);
                                    //i = 0;
                                    Thread.Sleep((int)(Distance(d.Position, d.Parcel.LocationOfSender) / speed));
                                    d.Parcel.distance = 0;
                                }
                                else
                                {
                                    Thread.Sleep(DELAY);
                                    d.Status = Status.FREE;
                                    bl.UpdateDrone(d);
                                    bl.DroneToCharge((int)d.ID);
                                    DateTime time = DateTime.Now;
                                    do
                                    {
                                        System.Threading.Thread.Sleep(500);
                                        d.Status = Status.MAINTENANCE;
                                        bl.UpdateDrone(d);
                                        bl.DroneOutCharge((int)d.ID, (DateTime.Now - time).TotalMinutes);
                                        d = bl.FindDrone((int)d.ID);
                                        d.Status = Status.BELONG;
                                        bl.UpdateDrone(d);
                                    } while (d.Battery <= 100);
                                }
                            }
                            catch (ParcelPastErroeException)
                            {
                                Thread.Sleep(DELAY);
                            }
                        }
                        break;
                    case Status.MAINTENANCE:
                        Thread.Sleep(DELAY);
                        if (d.Battery == 100)
                        {
                            bl.DroneOutCharge(droneId, 60);
                            //i = 0;
                            break;
                        }
                        //bl.AddBattery(droneId, (DELAY / 1000) * bl.ChargePerSecond);
                        //DateTime time = DateTime.Now;
                        //while (d.Battery < 100)
                        //{
                        //    Thread.Sleep(500);
                        //    bl.DroneOutCharge((int)d.ID, (DateTime.Now - time).TotalMinutes);
                        //}
                        DateTime dateTime = DateTime.Now;
                        do
                        {
                            Thread.Sleep(500);
                            d.Status = Status.MAINTENANCE;
                            bl.UpdateDrone(d);
                            bl.DroneOutCharge((int)d.ID, (DateTime.Now - dateTime).TotalMinutes);
                            d = bl.FindDrone((int)d.ID);
                            d.Status = Status.BELONG;
                            bl.UpdateDrone(d);
                        } while (d.Battery <= 100);
                        break;
                }
            }
        }
    }
}