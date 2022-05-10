using BLExceptions;
using BO;
using System;
using System.Linq;
using System.Threading;

//ntc the delay
namespace BL
{
    /// <summary>
    /// Defines the <see cref="Simulator" />.
    /// </summary>
    public class Simulator
    {
        /// <summary>
        /// Defines the speed.
        /// </summary>
        private readonly double speed = 60;//---km/h

        /// <summary>
        /// Defines the DELAY.
        /// </summary>
        private readonly int DELAY = 1000;// waiting time 1 sec(1000 mlsc)

        /// <summary>
        /// The Distance.
        /// </summary>
        /// <param name="a">The a<see cref="Location"/>.</param>
        /// <param name="b">The b<see cref="Location"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double Distance(Location a, Location b)
        {
            return Math.Sqrt(Math.Pow(a.Lattitude - b.Lattitude, 2) + Math.Pow(a.Longitude - b.Longitude, 2));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Simulator"/> class.
        /// </summary>
        /// <param name="droneId">The droneId<see cref="int"/>.</param>
        /// <param name="display">The display<see cref="Action"/>.</param>
        /// <param name="checker">The checker<see cref="Func{bool}"/>.</param>
        /// <param name="bl">The bl<see cref="BL"/>.</param>
        public Simulator(int droneId, Action display, Func<bool> checker, BL bl)//contractor
        {
            while (checker())
            {
                display();
                Drone drone;
                Thread.Sleep(2000);
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
                                    bl.DroneOutCharge((int)drone.ID);//need fixing
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
                                Thread.Sleep((int)(Distance(drone.Parcel.LocationOfSender, drone.Position) + 1000 / speed));
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
                                    Thread.Sleep((int)(Distance(drone.Parcel.LocationOfSender, drone.Parcel.LocationOftarget) + 1000 / speed));
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
                            drone = bl.FindDrone((int)drone.ID);
                            drone.Status = Status.FREE;
                            bl.UpdateDrone(drone);
                            display();
                        }
                        break;
                }
            }
        }
    }
}
