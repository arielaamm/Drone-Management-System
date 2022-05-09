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
            while (checker())
            {
                display();
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
                                display();
                            }
                            try
                            {
                                bl.AttacheDrone(droneId);
                                display();
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
                                try
                                {
                                    drone.Status = BO.Status.MAINTENANCE;
                                    bl.UpdateDrone(drone);
                                    bl.DroneOutCharge((int)drone.ID, DateTime.Now);//need fixing
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
                                display();

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
                                try
                                {
                                    bl.Parceldelivery(droneId);
                                    display();
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
                            DateTime dateTime = DateTime.Now;
                            Thread.Sleep(1000);
                            drone.Status = Status.MAINTENANCE;
                            bl.UpdateDrone(drone);
                            bl.DroneOutCharge((int)drone.ID, DateTime.Now);
                            bl.UpdateDrone(drone);
                            display();
                        }
                        break;
                }
            }
        }
    }
}