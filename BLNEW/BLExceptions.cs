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
    public class NotTheRightFormException : Exception
    {
        public NotTheRightFormException() :base() { }
        public NotTheRightFormException(string message) : base(message) { }
        public NotTheRightFormException(string message, Exception innerException) : base(message, innerException) { }
        public NotTheRightFormException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
    public class PutTheRightNumberException : Exception
    {
        public PutTheRightNumberException() { }
        public PutTheRightNumberException(string message) : base(message) { }
        public PutTheRightNumberException(string message, Exception innerException) : base(message, innerException) { }
        protected PutTheRightNumberException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneAloreadyAttachedException : Exception
    {
        public DroneAloreadyAttachedException() { }
        public DroneAloreadyAttachedException(string message) : base(message) { }
        public DroneAloreadyAttachedException(string message, Exception innerException) : base(message, innerException) { }
        public DroneAloreadyAttachedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
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
    public class DroneDontInChargingException : Exception
    {
        public DroneDontInChargingException() { }
        public DroneDontInChargingException(string message) : base(message) { }
        public DroneDontInChargingException(string message, Exception innerException) : base(message, innerException) { }
        public DroneDontInChargingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ThereIsNoParcelToAttachException : Exception
    {
        public ThereIsNoParcelToAttachException() { }
        public ThereIsNoParcelToAttachException(string message) : base(message) { }
        public ThereIsNoParcelToAttachException(string message, Exception innerException) : base(message, innerException) { }
        public ThereIsNoParcelToAttachException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class AlreadyPickedUpException : Exception
    {
        public AlreadyPickedUpException() { }
        public AlreadyPickedUpException(string message) : base(message) { }
        public AlreadyPickedUpException(string message, Exception innerException) : base(message, innerException) { }
        public AlreadyPickedUpException(SerializationInfo info, StreamingContext context) : base(info, context) { }
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
    public class DroneIsBusyException : Exception
    {
        public DroneIsBusyException() { }
        public DroneIsBusyException(string message) : base(message) { }
        public DroneIsBusyException(string message, Exception innerException) : base(message, innerException) { }
        public DroneIsBusyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class ThereIsNoParcelToAttachdException : Exception
    {
        public ThereIsNoParcelToAttachdException() { }
        public ThereIsNoParcelToAttachdException(string message) : base(message) { }
        public ThereIsNoParcelToAttachdException(string message, Exception innerException) : base(message, innerException) { }
        public ThereIsNoParcelToAttachdException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class CantDeleteException : Exception
    {
        public CantDeleteException() { }
        public CantDeleteException(string message) : base(message) { }
        public CantDeleteException(string message, Exception innerException) : base(message, innerException) { }
        public CantDeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneInActionException : Exception
    {
        public DroneInActionException() { }
        public DroneInActionException(string message) : base(message) { }
        public DroneInActionException(string message, Exception innerException) : base(message, innerException) { }
        public DroneInActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
