using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BL.BL;
using System.ComponentModel;
using BLExceptions;

namespace BL
{
    public class Simulator
    {
        double speed = 60;//---km/h
        int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(BL bl, int droneId, Action display, bool checker)//constractor
        {
            while (!checker)
            {
                Drone d = new Drone();
                d = bl.FindDrone(droneId);
                switch (d.Status)
                {
                    case Status.CREAT:
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
                        if (bl.Findparcel((int)(d.Parcel.ID)).PickedUp == null)
                        {
                            double time = Distance(d.Position, d.Parcel.LocationOfSender) / speed;//enough battery??
                            if (time < 1)
                            {
                                bl.PickUpParcel((int)(d.Parcel.ID));
                                //i = 0;
                                Thread.Sleep((int)(Distance(d.Position, d.Parcel.LocationOfSender) / speed));
                            }
                            else
                            {
                                Thread.Sleep(DELAY);
                                //bl.AddBattery(droneId, -bl.GetElectricityPerKM((DELAY / 1000) * speed, d.Parcel.weight));
                                //TODO bonus location
                            }

                        }
                        else
                        {
                          double  time = Distance(d.Position, d.Parcel.LocationOftarget) / speed;// - i;//enough battery??
                            if (time < 1)
                            {
                                bl.Parceldelivery((int)d.Parcel.ID);
                                //i = 0;
                                Thread.Sleep((int)(Distance(d.Position, d.Parcel.LocationOfSender) / speed));
                                d.Parcel.distance = 0;
                            }
                            else
                            {
                                Thread.Sleep(DELAY);
                                //d.Parcel.distance -= (DELAY / 1000) * speed;
                            }

                        }
                        break;
                    case Status.MAINTENANCE:
                        Thread.Sleep(DELAY);
                        if (d.Battery == 100)
                        {
                            bl.DroneOutCharge(droneId, 0.1);
                            //i = 0;
                            break;
                        }
                        //bl.AddBattery(droneId, (DELAY / 1000) * bl.ChargePerSecond);
                        break;
                }
            }
        }

    }
}