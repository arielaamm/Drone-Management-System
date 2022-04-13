using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BL.BL;
using System.ComponentModel;

namespace BL
{
    public class Simulator
    {
        double speed = 60;//---km/h
        int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(BL bl, int droneId, Action display, bool checker)//constractor
        {
            /*
            while (!checker)
            {
                Drone d = new Drone();
                d = bl.FindDrone(droneId);
                switch (d.Status)
                {
                    case Status.vacant:
                        try
                        {
                            bl.Attribution(droneId);
                        }
                        catch (BO.DroneHaveToLittleBattery)
                        {
                            try
                            {
                                bl.DroneToCharge(droneId);
                            }
                            catch
                            {
                                //TODO
                                throw new BO.CantBeNegative(droneId);
                            }
                        }
                        catch (BO.NoParcelMatch)
                        {
                            Thread.Sleep(DELAY);
                        }
                        break;
                    case DroneStatuses.Shipping:
                        if (myBl.GetParcel(MyDrone.MyParcel.Id).PickedUp == null)
                        {
                            time = myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelSenderLocation(MyDrone.MyParcel.Id)) / speed - i;
                            if (time < 1)
                            {
                                myBl.PickedParcelUp(MyDrone.MyParcel.Id);
                                i = 0;
                                Thread.Sleep((int)(myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelSenderLocation(MyDrone.MyParcel.Id)) / speed));
                            }
                            else
                            {
                                Thread.Sleep(delay);
                                myBl.AddBattery(droneId, -myBl.GetElectricityPerKM((delay / 1000) * speed, MyDrone.MyParcel.Weight));
                                //TODO bonus location
                            }

                        }
                        else
                        {
                            time = myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelTargetLocation(MyDrone.MyParcel.Id)) / speed - i;
                            if (time < 1)
                            {
                                myBl.ParcelDelivered(MyDrone.MyParcel.Id);
                                i = 0;
                                Thread.Sleep((int)(myBl.DistanceTo(MyDrone.MyLocation, myBl.GetParcelTargetLocation(MyDrone.MyParcel.Id)) / speed));
                                MyDrone.MyParcel.TransferDistance = 0;
                            }
                            else
                            {
                                Thread.Sleep(delay);
                                myBl.AddBattery(droneId, -myBl.GetElectricityPerKM((delay / 1000) * speed, MyDrone.MyParcel.Weight));
                                MyDrone.MyParcel.TransferDistance -= (delay / 1000) * speed;
                            }

                        }
                        break;
                    case DroneStatuses.maintenance:
                        Thread.Sleep(delay);
                        if (MyDrone.Battery == 100)
                        {
                            bl.OutOfCharge(droneId, 0.1);
                            i = 0;
                            break;
                        }
                        bl.AddBattery(droneId, (delay / 1000) * myBl.ChargePerSecond);
                        break;
                }
            }*/
        }

    }
}
