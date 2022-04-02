using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DALExceptionscs
{
    [Serializable]
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base() { }
        public AlreadyExistException(string message) : base(message) { }
        public AlreadyExistException(string message, Exception innerException) : base(message, innerException) { }
        public AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException() { }
        public DoesNotExistException(string message) : base(message) { }
        public DoesNotExistException(string message, Exception innerException) : base(message, innerException) { }
        public DoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneInMiddleActionException : Exception
    {
        public DroneInMiddleActionException() { }
        public DroneInMiddleActionException(string message) : base(message) { }
        public DroneInMiddleActionException(string message, Exception innerException) : base(message, innerException) { }
        public DroneInMiddleActionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DroneNotChargingException : Exception
    {
        public DroneNotChargingException() { }
        public DroneNotChargingException(string message) : base(message) { }
        public DroneNotChargingException(string message, Exception innerException) : base(message, innerException) { }
        public DroneNotChargingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ThereAreNoRoomException : Exception
    {
        public ThereAreNoRoomException() { }
        public ThereAreNoRoomException(string message) : base(message) { }
        public ThereAreNoRoomException(string message, Exception innerException) : base(message, innerException) { }
        public ThereAreNoRoomException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class PhoneIsUsedException : Exception
    {
        public PhoneIsUsedException() { }
        public PhoneIsUsedException(string message) : base(message) { }
        public PhoneIsUsedException(string message, Exception innerException) : base(message, innerException) { }
        public PhoneIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class NameIsUsedException : Exception
    {
        public NameIsUsedException() { }
        public NameIsUsedException(string message) : base(message) { }
        public NameIsUsedException(string message, Exception innerException) : base(message, innerException) { }
        public NameIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class DeleteException : Exception
    {
        public DeleteException() { }
        public DeleteException(string message) : base(message) { }
        public DeleteException(string message, Exception innerException) : base(message, innerException) { }
        public DeleteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    internal class XmlLoadException : Exception
    {
        public XmlLoadException() { }
        public XmlLoadException(string message) : base(message) { }
        public XmlLoadException(string message, Exception innerException) : base(message, innerException) { }
        protected XmlLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
    [Serializable]
    internal class XmlWriteException : Exception
    {
        public XmlWriteException() { }
        public XmlWriteException(string message) : base(message) { }
        public XmlWriteException(string message, Exception innerException) : base(message, innerException) { }
        protected XmlWriteException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}

