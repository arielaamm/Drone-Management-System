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
            : base(message, innerException) { }
        public AlreadyExistException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NotTheRightForm : Exception
    {
        public NotTheRightForm() :base() { }
        public NotTheRightForm(string message) : base(message) { }
        public NotTheRightForm(string message, Exception innerException) : base(message, innerException) { }
        protected NotTheRightForm(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
    public class ThereIsNoParcel : Exception
    {
        public ThereIsNoParcel() { }
        public ThereIsNoParcel(string message) : base(message) { }
        public ThereIsNoParcel(string message, Exception innerException) : base(message, innerException) { }
        public ThereIsNoParcel(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ParcelPastErroeException : Exception
    {
        public ParcelPastErroeException() { }
        public ParcelPastErroeException(string message) : base(message) { }
        public ParcelPastErroeException(string message, Exception innerException) : base(message, innerException) { }
        public ParcelPastErroeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
