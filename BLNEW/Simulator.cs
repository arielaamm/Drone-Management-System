using System;

namespace BL
{
    public class Simulator
    {
        private readonly double speed = 60;//---km/h
        private readonly int DELAY = 1000; // whaiting time 1 sec(1000 mlsc)
        public Simulator(BL bl, int droneId, Action display, bool checker)//constractor
        {
            //while (!checker)
            //{
            //    Drone d = new Drone();
            //    d = bl.FindDrone(droneId);
            //    switch (d.Status)
            //    {
            //        case Status.CREAT:
            //            try
            //            {
            //                bl.AttacheDrone(droneId);
            //            }
            //            catch (DontHaveEnoughPowerException)
            //            {
            //                try
            //                {
            //                    bl.DroneToCharge(droneId);
            //                }
            //                catch
            //                {
            //                    //TODO
            //                    //  throw new BO.CantBeNegative(droneId);
            //                }
            //            }
            //            catch (ThereIsNoParcelToAttachException)
            //            {
            //                Thread.Sleep(DELAY);
            //            }
            //            break;
            //        case Status.BELONG:
            //            if (bl.Findparcel((int)(d.Parcel.ID)).PickedUp == null)
            //            {
            //                double time = Distance(d.Position, bl.Findcustomer(bl.Findparcel((int)(d.Parcel.ID)).sender.ID).Position) / speed;
            //                if (time < 1)
            //                {
            //                    bl.PickUpParcel((int)(d.Parcel.ID));
            //                    //i = 0;
            //                    Thread.Sleep((int)(Distance(d.Position, bl.Findcustomer(bl.Findparcel((int)(d.Parcel.ID)).sender.ID).Position) / speed));
            //                }
            //                else
            //                {
            //                    Thread.Sleep(DELAY);
            //                    myBl.AddBattery(droneId, -bl.GetElectricityPerKM((delay / 1000) * speed, MyDrone.MyParcel.Weight));
            //                    //TODO bonus location
            //                }

            //            }
            //            else
            //            {
            //                time = DistanceTo(d.Position, bl.GetParcelTargetLocation(d.MyParcel.Id)) / speed - i;
            //                if (time < 1)
            //                {
            //                    myBl.ParcelDelivered(MyDrone.MyParcel.Id);
            //                    i = 0;
            //                    Thread.Sleep((int)(Distance(d.Position, bl.GetParcelTargetLocation(MyDrone.MyParcel.Id)) / speed));
            //                    MyDrone.MyParcel.TransferDistance = 0;
            //                }
            //                else
            //                {
            //                    Thread.Sleep(delay);
            //                    bl.AddBattery(droneId, -bl.GetElectricityPerKM((DELAY / 1000) * speed, MyDrone.MyParcel.Weight));
            //                    MyDrone.MyParcel.TransferDistance -= (DELAY / 1000) * speed;
            //                }

            //            }
            //            break;
            //        case Status.MAINTENANCE:
            //            Thread.Sleep(DELAY);
            //            if (d.Battery == 100)
            //            {
            //                bl.DroneOutCharge(droneId, 0.1);
            //                //i = 0;
            //                break;
            //            }
            //            bl.AddBattery(droneId, (DELAY / 1000) * bl.ChargePerSecond);
            //            break;
            //    }
            //}
        }

    }
}