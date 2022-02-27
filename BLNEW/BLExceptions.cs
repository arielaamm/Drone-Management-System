using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BLExceptions
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception innerException)
            : base(message, innerException) {
            throw new AlreadyExistException(message);
        }
        public AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NotTheRightForm : Exception
    {
        public NotTheRightForm() :base() { }
        public NotTheRightForm(string message) : base(message) { }
        public NotTheRightForm(string message, Exception innerException) : base(message, innerException) { }
        public NotTheRightForm(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    [Serializable]
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        protected DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class PutTheRightNumber : Exception
    {
        public PutTheRightNumber() { }
        public PutTheRightNumber(string message) : base(message) { }
        public PutTheRightNumber(string message, Exception innerException) : base(message, innerException) { }
        protected PutTheRightNumber(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneAloreadyAttached : Exception
    {
        public DroneAloreadyAttached() { }
        public DroneAloreadyAttached(string message) : base(message) { }
        public DroneAloreadyAttached(string message, Exception innerException) : base(message, innerException) { }
        public DroneAloreadyAttached(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DontHaveEnoughPowerException : Exception
    {
        public DontHaveEnoughPowerException() { }

        public DontHaveEnoughPowerException(string message) : base(message) { }

        public DontHaveEnoughPowerException(string message, Exception innerException) : base(message, innerException) { }
        public DontHaveEnoughPowerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneDontInCharging : Exception
    {
        public DroneDontInCharging() { }
        public DroneDontInCharging(string message) : base(message) { }
        public DroneDontInCharging(string message, Exception innerException) : base(message, innerException) { }
        public DroneDontInCharging(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ThereIsNoParcelToAttach : Exception
    {
        public ThereIsNoParcelToAttach() { }
        public ThereIsNoParcelToAttach(string message) : base(message) { }
        public ThereIsNoParcelToAttach(string message, Exception innerException) : base(message, innerException) { }
        public ThereIsNoParcelToAttach(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class AlreadyPickedUp : Exception
    {
        public AlreadyPickedUp() { }
        public AlreadyPickedUp(string message) : base(message) { }
        public AlreadyPickedUp(string message, Exception innerException) : base(message, innerException) { }
        public AlreadyPickedUp(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ParcelPastErroeException : Exception
    {
        public ParcelPastErroeException() { }
        public ParcelPastErroeException(string message) : base(message) { }
        public ParcelPastErroeException(string message, Exception innerException) : base(message, innerException) { }
        public ParcelPastErroeException(SerializationInfo info, StreamingContext context) : base(info, context) { }    
    }
    [Serializable]
    public class DroneIsBusy : Exception
    {
        public DroneIsBusy() { }
        public DroneIsBusy(string message) : base(message) { }
        public DroneIsBusy(string message, Exception innerException) : base(message, innerException) { }
        public DroneIsBusy(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
