using BLExceptions;
using BO;
using System;
using System.Threading;
using static BL.BL;
//ntc the delay

namespace BL
{
    public class Simulator
    {
        private readonly double speed = 60;//---km/h
        private readonly int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(BL bl, int droneId, Action display, bool checker)//constractor
        {
            while (!checker)
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
                        if (bl.Findparcel((int)d.Parcel.ID).PickedUp == null)
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
                                bl.DroneToCharge((int)d.ID);

                                //bl.AddBattery(droneId, -bl.GetElectricityPerKM((DELAY / 1000) * speed, d.Parcel.weight));
                                //TODO bonus location
                            }
                        }
                        break;
                    case Status.PICKUP:
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
                                //d.Parcel.distance -= (DELAY / 1000) * speed;
                                bl.DroneToCharge((int)d.ID);
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
                        DateTime time = DateTime.Now;
                        while (d.Battery < 100)
                        {
                            Thread.Sleep(500);
                            bl.DroneOutCharge((int)d.ID, (DateTime.Now - time).TotalMinutes);
                        }
                        DateTime dateTime = DateTime.Now;
                        do
                        {
                            System.Threading.Thread.Sleep(500);
                            d.Status = Status.MAINTENANCE;
                            bl.UpdateDrone(d);
                            bl.DroneOutCharge((int)d.ID, (DateTime.Now - dateTime).TotalMinutes);
                            d = bl.FindDrone((int)d.ID);
                            d.Status = Status.BELONG;
                            bl.UpdateDrone(d);
                        } while (d.Battery < 100);
                        break;
                }
            }
        }
    }
}